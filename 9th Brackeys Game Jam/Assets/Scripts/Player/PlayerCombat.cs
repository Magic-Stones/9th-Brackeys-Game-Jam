using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private enum State
    {
        Aiming,
        Attacking
    }
    private State _currentState;

    [SerializeField] public float attackRate = 1f;
    private float _nextAttackTime = 0f;

    private Vector3 _mousePosition;
    private Transform _aimHandle;
    private bool disableAnimations = false;

    public delegate void EventDel();
    public EventDel OnAttacking;
    private bool _activeOnAttacking = false;

    [Header("Instantiate Bullet")]
    [SerializeField] private Transform aimPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private LayerMask enemyLayer;

    private Animator _animator;
    private CharacterController2D _controller2D;
    private Player _player;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _controller2D = GetComponent<CharacterController2D>();
        _player = GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _aimHandle = transform.Find("Aim Handle");

        _player.OnBeginKnockback += () => disableAnimations = true;
        _player.OnDoneKnockback += () => disableAnimations = false;
        OnAttacking += Shoot;
    }

    // Update is called once per frame
    void Update()
    {
        CheckAimState();
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

        if (!disableAnimations)
        {
            if (_mousePosition.y > transform.position.y)
            {
                _animator.Play(_controller2D.Anim_FaceUp.name);
            }
            else if (_mousePosition.y < transform.position.y)
            {
                _animator.Play(_controller2D.Anim_FaceDown.name);
            }
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, aimPoint.position, aimPoint.rotation);
    }
}
