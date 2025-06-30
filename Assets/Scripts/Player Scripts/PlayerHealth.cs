using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerStateManager))]
public class PlayerHealth : Subject<SoundEvent>
{
    private SpriteRenderer spriteRenderer;

    private float maxHealth = 100f;
    private float currentHealth;
    
    private bool canTakeDamaged = true;
    private float takeDamageInterval = 2f;
    private float timer = 0;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;

        foreach (var observer in FindObjectsOfType<MonoBehaviour>())
        {
            if (observer is IObserver<SoundEvent> obs)
            {
                AddObserver(obs);
            }
        }
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

            var stateManager = GetComponent<PlayerStateManager>();
            if (currentHealth <= 0)
                stateManager.SwitchCurrentState(stateManager.deadState);
            else
                stateManager.SwitchCurrentState(stateManager.hurtState);

            canTakeDamaged = false;
            timer = 0;
            StartCoroutine(FlashSprite());
        }
    }

    public void FullHeal()
    {
        currentHealth = maxHealth;
    }

    IEnumerator FlashSprite()
    {
        float flashDuration = 0.2f;
        float elapsed = 0f;
        while (elapsed < takeDamageInterval)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(flashDuration / 2f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(flashDuration / 2f);
            elapsed += flashDuration;
        }

        spriteRenderer.enabled = true;
    }

    public float CurrentHealth => currentHealth;
    public bool CanTakeDamaged => canTakeDamaged;

}
