using UnityEngine;

[RequireComponent(typeof(Animator), typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class EnemyStateManager : MonoBehaviour
{
    State<EnemyStateManager> enemyCurrentState;

    public EnemyIdleState enemyIdleState = new EnemyIdleState();
    public EnemyRunState enemyRunState = new EnemyRunState();
    public EnemyShootState enemyShootState = new EnemyShootState();
    public EnemyDeadState enemyDeadState = new EnemyDeadState();

    private Animator animator;
    private bool isFacingRight = true;
    private Vector2 leftPoint;
    private Vector2 rightPoint;

    [Header("target player")]
    private Transform player;
    private float detectRange = 5f;

    public State<EnemyStateManager> EnemyCurrentState => enemyCurrentState;
    public bool IsFacingRight => isFacingRight;
    public Animator anim => animator;
    public Vector2 LeftPoint => leftPoint;
    public Vector2 RightPoint => rightPoint;
    public Transform Player => player;

    public void Initialize()
    {
        animator = GetComponent<Animator>();
        enemyCurrentState = enemyIdleState;
        enemyCurrentState.EnterState(this);

        leftPoint = new Vector2(transform.position.x - 2.5f, transform.position.y);
        rightPoint = new Vector2(transform.position.x + 2.5f, transform.position.y);

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        enemyCurrentState.UpdateState(this);
    }

    public void SwitchEnemyState(State<EnemyStateManager> enemyState)
    {
        enemyCurrentState = enemyState;
        enemyCurrentState.EnterState(this);
    }

    public void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        isFacingRight = !isFacingRight;
    }

    public bool CanSeePlayer()
    {
        return Vector3.Distance(transform.position, player.position) <= detectRange;
    }
}
