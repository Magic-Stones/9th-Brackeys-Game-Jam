using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int maxHealthPoints = 10;
    [SerializeField] private int _healthPoints;
    public float GetHealthPointsNormalized { get { return (float) _healthPoints / maxHealthPoints; } }

    [SerializeField] private float moveSpeed = 1f;
    
    public delegate void VoidDelegates();
    public VoidDelegates OnBeginKnockback, OnDoneKnockback;

    private Rigidbody2D _rb2D;
    private Animator _animator;
    private CharacterController2D _controller2D;

    void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _controller2D = GetComponent<CharacterController2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _healthPoints = maxHealthPoints;

        OnBeginKnockback += KnockbackEffect;

        _controller2D.ExternalMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        _healthPoints -= amount;

        if (_healthPoints <= 0)
        {
            Death();
        }
    }

    public void Knockback(Transform dealer, float knockbackPower)
    {
        StopAllCoroutines();
        OnBeginKnockback?.Invoke();

        Vector2 direction = (transform.position - dealer.position).normalized;
        _rb2D.AddForce(direction * knockbackPower, ForceMode2D.Impulse);

        float knockbackDelay = 0.5f;
        StartCoroutine(RecoverFromKnockback(knockbackDelay));
    }

    private IEnumerator RecoverFromKnockback(float delay)
    {
        yield return new WaitForSeconds(delay);
        _rb2D.velocity = Vector2.zero;
        OnDoneKnockback?.Invoke();
    }

    public void Death()
    {
        Debug.Log("DEAD");
    }

    private void KnockbackEffect()
    {
        _animator.Play(_controller2D.Anim_HurtDown.name);
    }
}
