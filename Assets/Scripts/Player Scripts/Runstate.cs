using UnityEngine;

public class RunState : State
{
    private Rigidbody2D rb;
    private float horizontal;
    private float moveSpeed = 5f;

    public override void EnterState(PlayerStateManager state)
    {
        rb = state.GetComponent<Rigidbody2D>();
        state.anim.SetBool("Running", true);
    }

    public override void UpdateState(PlayerStateManager state)
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        Vector2 aimDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - state.transform.position).normalized;

        state.anim.SetFloat("AimX", aimDirection.x);
        state.anim.SetFloat("AimY", aimDirection.y);

        if (horizontal < 0f && state.IsFacingRight || horizontal > 0f && !state.IsFacingRight)
        {
            state.Flip();
        }

        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        ExitState(state);
    }

    public override void ExitState(PlayerStateManager state)
    {
        if (horizontal == 0f)
        {
            state.anim.SetBool("Running", false);
            state.SwitchCurrentState(state.idleState);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            state.anim.SetBool("Running", false);
            state.SwitchCurrentState(state.jumpState);
        }

        if (Input.GetKey(KeyCode.S))
        {
            state.anim.SetBool("Running", false);
            state.SwitchCurrentState(state.crounchState);
        }
    }
}
