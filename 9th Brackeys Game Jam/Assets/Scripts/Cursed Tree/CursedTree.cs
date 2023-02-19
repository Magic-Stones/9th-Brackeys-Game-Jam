using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedTree : MonoBehaviour
{
    [SerializeField] private int maxHealthPoints = 100;
    private int _healthPoints;
    public float GetHealthPointsNormalized { get { return (float)_healthPoints / maxHealthPoints; } }

    public delegate void VoidDelegates();
    public VoidDelegates OnTreeDestroy;

    private float reduceHealth = 1f;
    public GameObject axe;

    // Start is called before the first frame update
    void Start()
    {
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

    public void ChopTree()
    {
        reduceHealth -= Time.deltaTime;
        _healthPoints = (int) reduceHealth;
    }

    private void Death()
    {
        OnTreeDestroy?.Invoke();
    }
}
