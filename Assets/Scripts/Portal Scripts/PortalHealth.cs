using System.Collections;
using UnityEngine;

public class PortalHealth : Subject
{
    private SpriteRenderer m_Renderer;
    private BoxCollider2D m_Collider;
    private Animator m_Animator;

    [SerializeField] private Sprite spriteBroke;
    private GameObject explosionEffect;

    private float health;
    private float maxHealth = 200f;

    void Start()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
        m_Collider = GetComponent<BoxCollider2D>();
        m_Animator = GetComponent<Animator>();
        Transform explosionTransform = transform.Find("ExplosionEffect");
        explosionEffect = explosionTransform != null ? explosionTransform.gameObject : null;
        health = maxHealth;

        if (explosionEffect != null)
            explosionEffect.SetActive(false);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            m_Collider.enabled = false;
            GetComponent<SpawnEnemy>().enabled = false;

            StartCoroutine(WaitExplosion());
        }
    }

    IEnumerator WaitExplosion()
    {
        if (explosionEffect != null)
        {
            explosionEffect.SetActive(true);
            m_Animator.SetTrigger("Explosion");
            yield return new WaitForSeconds(1f);
            m_Renderer.enabled = false;
            explosionEffect.gameObject.GetComponent<SpriteRenderer>().sprite = spriteBroke;
        }
    }
}
