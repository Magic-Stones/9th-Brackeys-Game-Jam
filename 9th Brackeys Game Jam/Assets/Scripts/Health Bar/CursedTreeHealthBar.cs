using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedTreeHealthBar : MonoBehaviour
{
    private Transform _healthBar;

    private CursedTree _cursedTree;

    // Start is called before the first frame update
    void Start()
    {
        _healthBar = transform.Find("Bar");

        _cursedTree = FindObjectOfType<CursedTree>();
    }

    // Update is called once per frame
    void Update()
    {
        SetHealth(_cursedTree.GetHealthPointsNormalized);
    }

    private void SetHealth(float health)
    {
        _healthBar.localScale = new Vector3(health, 1f);
    }
}
