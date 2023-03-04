using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private const float DISPLAY_DAMAGE_TIMER_MAX = 1f;
    private float _displayDamageTimer;
    private float _unfillAmountSpeed = 0.5f;

    private Image _healthBar;
    private Image _damageBar;

    private Player _player;

    void Awake()
    {
        _player = FindObjectOfType<Player>();

        _healthBar = transform.Find("Health Bar").GetComponent<Image>();
        _damageBar = transform.Find("Damage Bar").GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _damageBar.fillAmount = _healthBar.fillAmount;
        _displayDamageTimer = DISPLAY_DAMAGE_TIMER_MAX;
    }

    // Update is called once per frame
    void Update()
    {
        SetHealth(_player.GetHealthPointsNormalized);

        if (_healthBar.fillAmount < _damageBar.fillAmount)
        {
            _displayDamageTimer -= Time.deltaTime;
            if (_displayDamageTimer <= 0f)
            {
                _damageBar.fillAmount -= _unfillAmountSpeed * Time.deltaTime;
            }
        }
        else _displayDamageTimer = DISPLAY_DAMAGE_TIMER_MAX;
    }

    private void SetHealth(float health)
    {
        _healthBar.fillAmount = health;
    }
}
