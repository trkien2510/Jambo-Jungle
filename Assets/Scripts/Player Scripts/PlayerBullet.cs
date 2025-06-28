using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float speed = 10f;
    private float dmg = 15f;

    public void Initialize(Vector2 direction)
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        if (anim == null)
            anim = GetComponent<Animator>();

        anim.SetBool("Explosion", false);
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
            
        }
        if (collision.CompareTag("Turret"))
        {
            TurretHealth turretHealth = collision.GetComponent<TurretHealth>();
            turretHealth.takeDamage(dmg);
            gameObject.SetActive(false);
        }
        
    }

    IEnumerator OutOfTime()
    {
        yield return new WaitForSeconds(2f);
        anim.SetBool("Explosion", true);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
