using System.Collections;
using UnityEngine;

public class HurtState : State<PlayerStateManager>
{
    private bool canSwitchState = false;
    public override void EnterState(PlayerStateManager state)
    {
        canSwitchState = false;
        state.anim.SetBool("Hurt", true);
        state.anim.SetBool("Jumping", false);
        Rigidbody2D rb = state.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, rb.velocity.y);
        state.NotifyPlayerObservers(GameEvent.PlayerHurt);
        state.StartCoroutine(WaitEndAnim(state));
    }

    public override void UpdateState(PlayerStateManager state)
    {
        if (canSwitchState)
        {
            ExitState(state);

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                state.SwitchCurrentState(state.runState);
            }

            if (state.GetComponent<Rigidbody2D>().velocity.y == 0)
            {
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
    }

    public override void ExitState(PlayerStateManager state)
    {
        state.anim.SetBool("Hurt", false);

    }

    IEnumerator WaitEndAnim(PlayerStateManager state)
    {
        yield return new WaitForSeconds(0.2f);
        canSwitchState = true;
    }
}
