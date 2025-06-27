using UnityEngine;

public class JumpState : State<PlayerStateManager>
{
    private Rigidbody2D rb;
    private LayerMask ground;
    private float jumpForce = 15f;
    private float moveSpeed = 5f;
    private bool hasLeftGround = false;
    private float horizontal;

    public override void EnterState(PlayerStateManager state)
    {
        rb = state.GetComponent<Rigidbody2D>();
        ground = LayerMask.GetMask("Ground");
        state.anim.SetBool("Jumping", true);

        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        hasLeftGround = false;

        state.NotifyPlayerObservers(PlayerAction.jump);
    }

    public override void UpdateState(PlayerStateManager state)
    {
        Transform groundCheck = state.transform.Find("GroundCheck");
        Vector3 checkPos = groundCheck != null ? groundCheck.position : state.transform.position;
        bool isGrounded = Physics2D.OverlapBox(checkPos, new Vector2(0.1f, 0.1f), 0, ground);

        horizontal = Input.GetAxisRaw("Horizontal");

        if ((horizontal > 0f && !state.IsFacingRight) || (horizontal < 0f && state.IsFacingRight))
        {
            state.Flip();
        }

        Vector2 aimDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - state.transform.position).normalized;
        state.anim.SetFloat("AimX", aimDirection.x);
        state.anim.SetFloat("AimY", aimDirection.y);

        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        if (!isGrounded)
        {
            hasLeftGround = true;
        }
        if (isGrounded && hasLeftGround)
        {
            ExitState(state);
        }
    }

    public override void ExitState(PlayerStateManager state)
    {
        state.anim.SetBool("Jumping", false);
        if (horizontal == 0)
            state.SwitchCurrentState(state.idleState);
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            state.SwitchCurrentState(state.runState);
    }
}
