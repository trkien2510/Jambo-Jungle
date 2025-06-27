using System.Collections;
using UnityEngine;

public class DeadState : State<PlayerStateManager>
{
    private bool isDeadSequence = false;

    public override void EnterState(PlayerStateManager state)
    {
        if (isDeadSequence) return;
        isDeadSequence = true;

        state.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        state.anim.SetTrigger("Defeat");
        state.StartCoroutine(WaitToDead(state));
    }

    public override void UpdateState(PlayerStateManager state)
    {
    }

    public override void ExitState(PlayerStateManager state)
    {
        state.anim.SetTrigger("Dead");
    }

    IEnumerator WaitToDead(PlayerStateManager state)
    {
        yield return new WaitForSeconds(1f);
        ExitState(state);
    }
}
