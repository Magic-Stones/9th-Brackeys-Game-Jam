using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    Vector3 Position { get; }
    void TakeDamage(int amount);
    void Knockback(Transform dealer, float knockbackPower);
}
