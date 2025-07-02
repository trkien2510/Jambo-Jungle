using UnityEngine;

public class TurretStateManager : Subject
{
    State<TurretStateManager> currentState;

    public TurretNomalState nomalState = new TurretNomalState();
    public TurretBrokeState brokeState = new TurretBrokeState();

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentState = nomalState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchBrokeState()
    {
        NotifyObserver(GameEvent.Explosion);
        currentState = brokeState;
        currentState.EnterState(this);
        GetComponent<TurretHealth>().enabled = false;
        GetComponent<TurretSpawnBullet>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public Animator anim => animator;
}