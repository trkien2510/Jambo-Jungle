using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    State currentState;
    State actionState;

    public IdleState idleState = new IdleState();
    public CrounchState crounchState = new CrounchState();
    public RunState runState = new RunState();
    public JumpState jumpState = new JumpState();
    public DeadState deadState = new DeadState();
    public ShootState shootState = new ShootState();
    public NoneState noneState = new NoneState();

    private Animator animator;
    private bool isFacingRight = true;

    private void Start()
    {
        currentState = idleState;
        actionState = noneState;
        currentState.EnterState(this);

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        currentState.UpdateState(this);
        actionState.UpdateState(this);
    }

    public void SwitchCurrentState(State state)
    {
        currentState = state;
        state.EnterState(this);
        Debug.Log(state);
    }

    public void SwitchActionState(State state)
    {
        actionState = state;
        state.EnterState(this);
    }

    public bool IsFacingRight => isFacingRight;

    public Animator anim => animator;

    public void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        isFacingRight = !isFacingRight;
    }
}
