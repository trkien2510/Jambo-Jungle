using UnityEngine;

public class BossStateManager : Subject
{
    State<BossStateManager> currentState;

    public BossIdleState bossIdleState = new BossIdleState();
    public BossBulletState bossBulletState = new BossBulletState();
    public BossLaserState bossLaserState = new BossLaserState();

    private Animator animator;
    [SerializeField] GameObject mouth;
    [SerializeField] GameObject leftEye;
    [SerializeField] GameObject rightEye;

    [SerializeField] GameObject laserHeadPrefab;
    [SerializeField] GameObject laserBodyPrefab;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentState = bossIdleState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchBossState(State<BossStateManager> state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void NotifyBossState(GameEvent action)
    {
        NotifyObserver(action);
    }

    public Animator anim => animator;
    public GameObject Mouth => mouth;
    public GameObject LeftEye => leftEye;
    public GameObject RightEye => rightEye;

    public GameObject LaserHead => laserHeadPrefab;
    public GameObject LaserBody => laserBodyPrefab;
}
