using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private int maxHealthPoints = 10;
    [SerializeField] private int _healthPoints;
    public float GetHealthPointsNormalized { get { return (float) _healthPoints / maxHealthPoints; } }

    [SerializeField] private float moveSpeed = 1f;
    public float GetMoveSpeed { get { return moveSpeed; } }

    public UnityEvent OnBegin, OnDone;

    private Rigidbody2D _rb2D;

    // Start is called before the first frame update
    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();

        _healthPoints = maxHealthPoints;
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
        OnBegin?.Invoke();

        Vector2 direction = (transform.position - dealer.position).normalized;
        _rb2D.AddForce(direction * knockbackPower, ForceMode2D.Impulse);

        StartCoroutine(RecoverFromKnockback(0.5f));
    }

    private IEnumerator RecoverFromKnockback(float delay)
    {
        yield return new WaitForSeconds(delay);
        _rb2D.velocity = Vector2.zero;
        OnDone?.Invoke();
    }

    public void Death()
    {
        Debug.Log("DEAD");
    }
}
