using UnityEngine;

[RequireComponent(typeof(TurretStateManager))]
public class TurretHealth : MonoBehaviour
{
    private float health;
    private float maxHealth = 200f;

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
            GetComponent<TurretStateManager>().SwitchBrokeState();
        }
    }
}
