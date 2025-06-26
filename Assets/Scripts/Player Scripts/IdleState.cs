using UnityEngine;

public class IdleState : State
{
    private float horizontal;
    public override void EnterState(PlayerStateManager state)
    {
        
    }

    public override void UpdateState(PlayerStateManager state)
    {
        Vector2 aimDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - state.transform.position).normalized;

        state.anim.SetFloat("AimX", aimDirection.x);
        state.anim.SetFloat("AimY", aimDirection.y);

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
        if (Input.GetKey(KeyCode.W))
        {
            state.SwitchCurrentState(state.jumpState);
        }
        if (Input.GetKey(KeyCode.S))
        {
            state.SwitchCurrentState(state.crounchState);
        }
    }
}
