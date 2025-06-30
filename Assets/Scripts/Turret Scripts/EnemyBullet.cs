using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float speed = 10f;
    [SerializeField] float dmg;

    public void Initialize(Vector2 direction)
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        if(anim == null)
            anim = GetComponent<Animator>();

        anim.enabled = true;
        anim.SetBool("Explosion", false);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        rb.velocity = direction.normalized * speed;

        StartCoroutine(OutOfTime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth.CanTakeDamaged)
            {
                playerHealth.TakeDamage(dmg);
                gameObject.SetActive(false);
            }
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            StartCoroutine(TouchGround());
        }
    }

    public void SetSpeed(float s)
    {
        speed = s;
    }


    IEnumerator TouchGround()
    {
        rb.velocity = Vector2.zero;
        anim.SetBool("Explosion", true);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    IEnumerator OutOfTime()
    {
        yield return new WaitForSeconds(2f);
        anim.SetBool("Explosion", true);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
