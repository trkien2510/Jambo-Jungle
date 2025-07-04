using UnityEngine;

public class RunState : State<PlayerStateManager>
{
    private Rigidbody2D rb;
    private float horizontal;
    private float moveSpeed = 5f;

    private float stepTimer = 0f;
    private float stepInterval = 0.3f;

    public override void EnterState(PlayerStateManager state)
    {
        rb = state.GetComponent<Rigidbody2D>();
        state.anim.SetBool("Running", true);
        state.NotifyPlayerObservers(GameEvent.PlayerRun);
        stepTimer = 0f;
    }

    public override void UpdateState(PlayerStateManager state)
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        state.UpdateAim();

        if (horizontal < 0f && state.IsFacingRight || horizontal > 0f && !state.IsFacingRight)
        {
            state.Flip();
        }

        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        if (Mathf.Abs(horizontal) > 0.1f)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepInterval)
            {
                state.NotifyPlayerObservers(GameEvent.PlayerRun);
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = stepInterval;
        }

        ExitState(state);
    }

    public override void ExitState(PlayerStateManager state)
    {
        if (horizontal == 0f)
        {
            state.anim.SetBool("Running", false);
            state.SwitchCurrentState(state.idleState);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
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
