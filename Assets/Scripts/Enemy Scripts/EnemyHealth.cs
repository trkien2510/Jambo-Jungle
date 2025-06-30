using UnityEngine;

[RequireComponent(typeof(EnemyStateManager))]
public class EnemyHealth : Subject<SoundEvent>
{
    private float enemyHealth;
    private float enemyMaxHealth = 100f;

    private EnemyStateManager stateManager;

    void Start()
    {
        enemyHealth = enemyMaxHealth;
        stateManager = GetComponent<EnemyStateManager>();

        foreach (var observer in FindObjectsOfType<MonoBehaviour>())
        {
            if (observer is IObserver<SoundEvent> obs)
            {
                AddObserver(obs);
            }
        }
    }

    public void TakeDamage(float amount)
    {
        enemyHealth -= amount;

        if (enemyHealth <= 0)
        {
            NotifyObserver(SoundEvent.explosion);
            stateManager.SwitchEnemyState(stateManager.enemyDeadState);
        }
    }
}
