using UnityEngine;


[RequireComponent (typeof(Animator), typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Barrel : Subject<SoundEvent>
{
    private Animator anim;
    private BoxCollider2D boxCol;
    private Rigidbody2D rb;

    private float health;
    private float maxHealth = 60f;
    void Start()
    {
        anim = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        health = maxHealth;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1f;

        foreach (var observer in FindObjectsOfType<MonoBehaviour>())
        {
            if (observer is IObserver<SoundEvent> obs)
            {
                AddObserver(obs);
            }
        }
    }

    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            NotifyObserver(SoundEvent.explosion);
            boxCol.enabled = false;
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
            anim.SetTrigger("Explosion");
            Destroy(gameObject, 1f);
        }
    }
}
