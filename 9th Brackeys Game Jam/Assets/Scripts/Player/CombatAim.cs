using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAim : MonoBehaviour
{
    [SerializeField] public float attackRate = 1f;
    private float _nextAttackTime = 0f;

    [SerializeField] private AnimationClip faceUp, faceDown;
    
    private Vector3 _mousePosition;
    private Transform _aimHandle;
    private Animator animator;

    public enum State
    {
        Aiming,
        Attacking
    }
    private State _currentState;

    [SerializeField] private Transform aimPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] private float _triggerChopRange = 1f;
    [SerializeField] private LayerMask _cursedTreeLayer;

    public delegate void VoidDelegates();
    public VoidDelegates OnAttacking;
    private bool _activeOnAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        _aimHandle = transform.Find("Aim Handle");
        OnAttacking += Shoot;
    }

    // Update is called once per frame
    void Update()
    {
        CheckAimState();
        // TriggerChop();
    }

    private void CheckAimState()
    {
        switch (_currentState)
        {
            case State.Aiming:

                AimDirection();

                FaceDirection();

                if (Input.GetMouseButtonDown(0))
                {
                    _currentState = State.Attacking;
                }

                break;

            case State.Attacking:

                if (Time.time >= _nextAttackTime)
                {
                    if (!_activeOnAttacking)
                    {
                        OnAttacking?.Invoke();
                        _activeOnAttacking = true;
                    }

                    _nextAttackTime = Time.time + 1f / attackRate;
                }
                else
                {
                    _activeOnAttacking = false;
                    _currentState = State.Aiming;
                }

                break;
        }
    }

    private void AimDirection()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 aimDirection = (_mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        _aimHandle.eulerAngles = new Vector3(0f, 0f, angle);

        Vector3 aimLocalScale = Vector3.one;
        if (angle > 90f || angle < -90f)
        {
            aimLocalScale.y = -1f;
        }
        else
        {
            aimLocalScale.y = 1f;
        }
        _aimHandle.localScale = aimLocalScale;
    }

    private void FaceDirection()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (_mousePosition.y > transform.position.y)
        {
            animator.Play(faceUp.name);
        }
        else if (_mousePosition.y < transform.position.y)
        {
            animator.Play(faceDown.name);
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, aimPoint.position, aimPoint.rotation);
    }

    private void TriggerChop()
    {
        Collider2D[] greatTree = Physics2D.OverlapCircleAll(transform.position, _triggerChopRange, _cursedTreeLayer);
        foreach (Collider2D tree in greatTree)
        {
            CursedTree cursedTree = tree.GetComponent<CursedTree>();
            if (cursedTree != null)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    cursedTree.ChopTree();
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    cursedTree.axe.SetActive(true);
                }
                else if (Input.GetKeyUp(KeyCode.E))
                {
                    cursedTree.axe.SetActive(false);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _triggerChopRange);
    }
}
