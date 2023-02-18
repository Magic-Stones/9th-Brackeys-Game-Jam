using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedTree : MonoBehaviour
{
    [SerializeField] private int _healthPoints = 100;
    private float reduceHealth = 1f;
    public GameObject axe;

    // Start is called before the first frame update
    void Start()
    {
        reduceHealth = _healthPoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChopTree()
    {
        reduceHealth -= Time.deltaTime;
        _healthPoints = (int) reduceHealth;
    }
}
