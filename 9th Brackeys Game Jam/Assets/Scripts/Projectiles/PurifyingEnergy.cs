using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurifyingEnergy : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float projectileSpeed = 10f;

    private bool goRandomDirection = false;

    private Rigidbody2D rb2D;
    private Transform _cursedTreeTransform;
    private CursedTree _cursedTree;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        _cursedTreeTransform = GameObject.FindGameObjectWithTag("Cursed Tree").transform;
        _cursedTree = FindObjectOfType<CursedTree>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        TravelProjectile();
    }

    private void TravelProjectile()
    {
        if (_cursedTreeTransform != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, _cursedTreeTransform.position, projectileSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Cursed Great Tree"))
        {
            _cursedTree.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
