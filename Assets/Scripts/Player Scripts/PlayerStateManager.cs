using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateManager : Subject
{
    State<PlayerStateManager> currentState;

    public IdleState idleState = new IdleState();
    public CrounchState crounchState = new CrounchState();
    public RunState runState = new RunState();
    public JumpState jumpState = new JumpState();
    public DeadState deadState = new DeadState();
    public HurtState hurtState = new HurtState();

    private Animator animator;
    private bool isFacingRight = true;

    private void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchCurrentState(State<PlayerStateManager> state)
    {
        currentState = state;
        state.EnterState(this);
        Debug.Log(state);
    }

    public void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        isFacingRight = !isFacingRight;
    }

    public void NotifyPlayerObservers(SoundEvent action)
    {
        NotifyObserver(action);
    }


    public bool IsFacingRight => isFacingRight;
    public Animator anim => animator;
    public State<PlayerStateManager> CurrentState => currentState;
}
