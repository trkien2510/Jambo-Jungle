using UnityEngine;

[RequireComponent(typeof(BossStateManager))]
public class BossHealth : Subject
{
    private float health;
    private float maxHealth = 5000f;

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
        NotifyObserver(GameEvent.BossDamaged);
        if (health <= 0)
        {
            NotifyObserver(GameEvent.BossDefeated);
        }
    }

    public float Health => health;
    public float MaxHealth => maxHealth;
}
