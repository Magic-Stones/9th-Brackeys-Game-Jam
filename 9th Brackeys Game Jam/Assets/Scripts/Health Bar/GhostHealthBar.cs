using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostHealthBar : MonoBehaviour
{
    private Transform _healthBar;

    private GhostAI _ghostAI;
    
    // Start is called before the first frame update
    void Start()
    {
        _healthBar = transform.Find("Bar");

        _ghostAI = FindObjectOfType<GhostAI>();
    }

    // Update is called once per frame
    void Update()
    {
        SetHealth(_ghostAI.GetHealthPointsNormalized);
    }

    private void SetHealth(float health)
    {
        _healthBar.localScale = new Vector3(health, 1f);
    }
}
