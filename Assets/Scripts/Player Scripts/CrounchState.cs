using UnityEngine;

public class CrounchState : State<PlayerStateManager>
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    private Vector2 originalSize;
    private Vector2 originalOffset;

    private Vector2 crouchSize = new Vector2(0.7f, 0.3f);
    private Vector2 crouchOffset = new Vector2(0f, -0.275f);

    public override void EnterState(PlayerStateManager state)
    {
        state.anim.SetBool("Crounch", true);

        rb = state.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;

        boxCollider = state.GetComponent<BoxCollider2D>();
        originalSize = boxCollider.size;
        originalOffset = boxCollider.offset;
        boxCollider.size = crouchSize;
        boxCollider.offset = crouchOffset;
    }

    public override void UpdateState(PlayerStateManager state)
    {
        if (Input.GetKeyDown(KeyCode.A) && state.IsFacingRight)
        {
            state.Flip();
        }
        if (Input.GetKeyDown(KeyCode.D) && !state.IsFacingRight)
        {
            state.Flip();
        }

        if (!Input.GetKey(KeyCode.S))
        {
            ExitState(state);
            state.SwitchCurrentState(state.idleState);
        }
    }

    public override void ExitState(PlayerStateManager state)
    {
        boxCollider.size = originalSize;
        boxCollider.offset = originalOffset;
        state.anim.SetBool("Crounch", false);
    }
}
