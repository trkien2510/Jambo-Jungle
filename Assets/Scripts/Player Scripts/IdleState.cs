using UnityEngine;

public class IdleState : State<PlayerStateManager>
{
    private float horizontal;
    public override void EnterState(PlayerStateManager state)
    {
        
    }

    public override void UpdateState(PlayerStateManager state)
    {
        state.UpdateAim();

        horizontal = Input.GetAxisRaw("Horizontal");

        ExitState(state);
    }

    public override void ExitState(PlayerStateManager state)
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (horizontal != 0)
            {
                state.SwitchCurrentState(state.runState);
            }
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
        {
            state.SwitchCurrentState(state.jumpState);
        }
        if (Input.GetKey(KeyCode.S))
        {
            state.SwitchCurrentState(state.crounchState);
        }
    }
}
