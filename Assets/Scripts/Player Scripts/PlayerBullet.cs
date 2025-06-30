using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float speed = 15f;
    private float dmg = 20f;

    public void Initialize(Vector2 direction)
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        if (anim == null)
            anim = GetComponent<Animator>();

        anim.Rebind();
        anim.SetBool("OutOfTime", false);
        anim.enabled = true;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        rb.velocity = direction.normalized * speed;

        StartCoroutine(OutOfTime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(dmg);
            StartCoroutine(Impact());
        }
        if (collision.CompareTag("Turret"))
        {
            TurretHealth turretHealth = collision.GetComponent<TurretHealth>();
            turretHealth.TakeDamage(dmg);
            StartCoroutine(Impact());
        }
        if (collision.CompareTag("Portal"))
        {
            PortalHealth portalHealth = collision.GetComponent<PortalHealth>();
            portalHealth.TakeDamage(dmg);
            StartCoroutine(Impact());
        }
        if (collision.CompareTag("Barrel"))
        {
            Barrel barrelHealth = collision.GetComponent<Barrel>();
            barrelHealth.TakeDamage(dmg);
            StartCoroutine(Impact());
        }
        if (collision.CompareTag("Boss"))
        {
            BossHealth bossHealth = collision.GetComponent<BossHealth>();
            bossHealth.TakeDamage(dmg);
            StartCoroutine(Impact());
        }
    }

    IEnumerator Impact()
    {
        rb.velocity = Vector2.zero;
        anim.SetTrigger("Impact");
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    IEnumerator OutOfTime()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("OutOfTime", true);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
