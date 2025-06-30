using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossStateManager))]
public class BossHealth : MonoBehaviour
{
    private float health;
    private float maxHealth = 2000f;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Debug.Log("boss dead");
        }
    }
}
