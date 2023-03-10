using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [Range(0f, 1f)]
    [SerializeField] private float damageReduction;

    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float knockbackPower = 1f;
    [SerializeField] private float startLifetime = 1f;
    private float lifetime;
    private Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        lifetime = startLifetime;
        rb2D.velocity = transform.right * projectileSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy"))
        {
            IEnemy enemy = collision.GetComponent<IEnemy>();
            Rigidbody2D enemyrb = collision.GetComponent<Rigidbody2D>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                DamagePopUp.Instantiate(enemy.Position, damage);

                enemy.Knockback(transform, knockbackPower);

                damage = Mathf.CeilToInt(damage * damageReduction);
            }
        }
    }
}
