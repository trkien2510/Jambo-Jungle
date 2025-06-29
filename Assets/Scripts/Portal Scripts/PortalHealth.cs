using System.Collections;
using UnityEngine;

public class PortalHealth : Subject
{
    private SpriteRenderer rend;
    private BoxCollider2D col;
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private Sprite spriteBroke;
    private GameObject explosionEffect;

    private float health;
    private float maxHealth = 200f;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Transform explosionTransform = transform.Find("ExplosionEffect");
        explosionEffect = explosionTransform != null ? explosionTransform.gameObject : null;
        health = maxHealth;

        if (explosionEffect != null)
            explosionEffect.SetActive(false);

        foreach (var observer in FindObjectsOfType<MonoBehaviour>())
        {
            if (observer is IObserver obs)
            {
                AddObserver(obs);
            }
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            col.enabled = false;
            GetComponent<SpawnEnemy>().enabled = false;

            StartCoroutine(WaitExplosion());
        }
    }

    IEnumerator WaitExplosion()
    {
        if (explosionEffect != null)
        {
            NotifyObserver(SoundEvent.explosion);
            explosionEffect.SetActive(true);
            anim.SetTrigger("Explosion");
            rb.bodyType = RigidbodyType2D.Kinematic;
            yield return new WaitForSeconds(1f);
            rend.enabled = false;
            explosionEffect.gameObject.GetComponent<SpriteRenderer>().sprite = spriteBroke;
        }
    }
}
