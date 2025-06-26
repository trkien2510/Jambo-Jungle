using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D), typeof(Animator))]
public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    private Animator anim;
    private float speed = 5f;

    public void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();

        anim.enabled = true;
        anim.SetBool("Explosion", false);

        rb.velocity = transform.right * speed;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();

        anim.enabled = true;
        anim.SetBool("Explosion", false);

        rb.velocity = transform.right * speed;
    }

    private void Update()
    {
        //StartCoroutine(BulletExplosion());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            circleCollider.enabled = false;
            gameObject.SetActive(false);
        }
    }

    IEnumerator BulletExplosion()
    {
        rb.velocity = Vector2.zero;
        anim.SetBool("Explosion", true);
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("Explosion", false);
        anim.enabled = false;
        circleCollider.enabled = true;
        gameObject.SetActive(false);
    }
}
