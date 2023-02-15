using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAI : MonoBehaviour, IEnemy
{
    [SerializeField] private int healthPoints = 100;
    [SerializeField] private float moveSpeed = 1f;
    private float additionalMoveSpeed;

    [SerializeField] private int damage = 1;
    [SerializeField] private float knockbackPower = 1f;

    [SerializeField] private GameObject targetObject;
    private Transform target;

    [SerializeField] private AnimationClip faceUp, faceDown;
    private bool isMoving = true;
    private float setWaitToMove = 2f, waitToMove;

    [SerializeField] private GameObject lootDrop;
    private bool lootDropChance;
    
    private Rigidbody2D rb2D;
    private Animator animator;

    public Vector3 Position()
    {
        return transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        additionalMoveSpeed = moveSpeed + 1.25f;
        moveSpeed = Random.Range(moveSpeed, additionalMoveSpeed);

        waitToMove = setWaitToMove;

        target = GameObject.FindGameObjectWithTag(targetObject.tag).transform;
        lootDropChance = 0.75f > Random.Range(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        FacingTarget();
    }

    private void FixedUpdate()
    {
        ChaseTarget();
    }

    public void TakeDamage(int amount)
    {
        healthPoints -= amount;

        if (healthPoints <= 0)
        {
            Death();
        }
    }

    public void ChaseTarget()
    {
        if (target != null)
        {
            if (isMoving)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.fixedDeltaTime);
            }
            else
            {
                waitToMove -= Time.deltaTime;
                if (waitToMove <= 0f)
                {
                    waitToMove = setWaitToMove;
                    isMoving = true;
                }
            }
        }
    }

    public void Knockback(Transform dealer, float knockbackPower)
    {
        StopAllCoroutines();
        rb2D.isKinematic = false;

        Vector2 direction = (transform.position - dealer.position).normalized;
        float knockbackDelay = knockbackPower * 0.5f;
        rb2D.AddForce(direction * knockbackPower, ForceMode2D.Impulse);

        StartCoroutine(RecoverFromKnockback(knockbackDelay));
    }

    private IEnumerator RecoverFromKnockback(float delay)
    {
        yield return new WaitForSeconds(delay);
        rb2D.velocity = Vector2.zero;   
        rb2D.isKinematic = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerStatus player;
        if (collision.collider.name.Equals(targetObject.name))
        {
            isMoving = false;

            player = collision.collider.GetComponent<PlayerStatus>();
            player.TakeDamage(damage);
            player.Knockback(transform, knockbackPower);
        }
    }

    public void Death()
    {
        if (lootDropChance) Instantiate(lootDrop, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    public void FacingTarget()
    {
        if (transform.position.y > target.position.y)
        {
            animator.Play(faceDown.name);
        }
        else if (transform.position.y < target.position.y)
        {
            animator.Play(faceUp.name);
        }
    }

    private void SetAnimator()
    {
        // animator.SetBool("isMoving", isMoving);
    }
}
