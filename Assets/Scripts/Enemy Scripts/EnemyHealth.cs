using UnityEngine;

[RequireComponent(typeof(EnemyStateManager))]
public class EnemyHealth : MonoBehaviour
{
    private float enemyHealth;
    private float enemyMaxHealth = 100f;

    private EnemyStateManager stateManager;

    void Start()
    {
        enemyHealth = enemyMaxHealth;
        stateManager = GetComponent<EnemyStateManager>();
    }

    public void TakeDamage(float amount)
    {
        enemyHealth -= amount;

        if (enemyHealth <= 0)
        {
            stateManager.SwitchEnemyState(stateManager.enemyDeadState);
        }
    }
}
