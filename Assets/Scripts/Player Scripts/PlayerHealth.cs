using UnityEngine;

[RequireComponent(typeof(PlayerStateManager))]
public class PlayerHealth : Subject
{
    private float health = 100f;
    private float currentHealth;
    
    private bool canTakeDamaged = true;
    private float takeDamageInterval = 2f;
    private float timer = 0;

    private void Start()
    {
        currentHealth = health;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= takeDamageInterval)
        {
            canTakeDamaged = true;
            timer = 0;
        }
    }

    public void TakeDamage(float amount)
    {
        if (canTakeDamaged)
        {
            currentHealth -= amount;

            if (currentHealth <= 0)
            {
                GetComponent<PlayerStateManager>().SwitchCurrentState(GetComponent<PlayerStateManager>().deadState);
            }
            else
            {
                GetComponent<PlayerStateManager>().SwitchCurrentState(GetComponent<PlayerStateManager>().hurtState);
            }
            canTakeDamaged= false;
            timer = 0;
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, 100);
    }

    public float CurrentHealth => currentHealth;
}
