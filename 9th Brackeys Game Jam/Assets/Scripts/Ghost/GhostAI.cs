using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAI : MonoBehaviour, IEnemy
{
    [SerializeField] private int maxHealthPoints = 100;
    private int _healthPoints;
    public float GetHealthPointsNormalized { get { return (float)_healthPoints / maxHealthPoints; } }

    [SerializeField] private float moveSpeed = 1f;

    [SerializeField] private int damage = 1;
    [SerializeField] private float knockbackPower = 1f;

    public Vector3 Position { get { return transform.position; } }

    [SerializeField] private GameObject setTarget;
    private Transform _target;

    [SerializeField] private AnimationClip faceUp, faceDown;
    private bool _isMoving = true;
    private float _setWaitToMove = 2f, _waitToMove;

    [SerializeField] private GameObject lootDrop;
    private bool _lootDropChance;
    
    private Rigidbody2D _rb2D;
    private Animator _animator;
    private EnemySpawner _enemySpawner;
    private GameHandler handler;

    void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _healthPoints = maxHealthPoints;

        float _additionalMoveSpeed = moveSpeed + 1.75f;
        moveSpeed = Random.Range(moveSpeed, _additionalMoveSpeed);

        _waitToMove = _setWaitToMove;

        _target = GameObject.FindGameObjectWithTag(setTarget.tag).transform;
        _lootDropChance = 0.75f > Random.Range(0f, 1f);
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
        _healthPoints -= amount;

        if (_healthPoints <= 0)
        {
            Death();
        }
    }

    public void ChaseTarget()
    {
        if (_target != null)
        {
            if (_isMoving)
            {
                transform.position = Vector2.MoveTowards(transform.position, _target.position, moveSpeed * Time.fixedDeltaTime);
            }
            else
            {
                _waitToMove -= Time.deltaTime;
                if (_waitToMove <= 0f)
                {
                    _waitToMove = _setWaitToMove;
                    _isMoving = true;
                }
            }
        }
    }

    public void Knockback(Transform dealer, float knockbackPower)
    {
        StopAllCoroutines();
        _rb2D.isKinematic = false;

        Vector2 direction = (transform.position - dealer.position).normalized;
        float knockbackDelay = 0.5f;
        _rb2D.AddForce(direction * knockbackPower, ForceMode2D.Impulse);

        StartCoroutine(RecoverFromKnockback(knockbackDelay));
    }

    private IEnumerator RecoverFromKnockback(float delay)
    {
        yield return new WaitForSeconds(delay);
        _rb2D.velocity = Vector2.zero;   
        _rb2D.isKinematic = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name.Equals(setTarget.name))
        {
            _isMoving = false;

            Player player = collision.collider.GetComponent<Player>();
            player.TakeDamage(damage);
            player.Knockback(transform, knockbackPower);
        }
    }

    public void Death()
    {
        // if (_lootDropChance)
        Instantiate(lootDrop, transform.position, Quaternion.identity);
        _enemySpawner.ReduceSpawnCount();

        Destroy(gameObject);
    }

    public void FacingTarget()
    {
        if (transform.position.y > _target.position.y)
        {
            _animator.Play(faceDown.name);
        }
        else if (transform.position.y < _target.position.y)
        {
            _animator.Play(faceUp.name);
        }
    }

    private void SetAnimator()
    {
        // animator.SetBool("isMoving", isMoving);
    }
}
