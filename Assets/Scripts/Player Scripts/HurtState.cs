using System.Collections;
using UnityEngine;

public class HurtState : State
{
    public override void EnterState(PlayerStateManager state)
    {
        state.anim.SetTrigger("Hurt");
        state.StartCoroutine(WaitEndAnim(state));
    }

    public override void ExitState(PlayerStateManager state)
    {
        state.anim.SetTrigger("Hurt");
    }

    public override void UpdateState(PlayerStateManager state) { }

    IEnumerator WaitEndAnim(PlayerStateManager state)
    {
        yield return new WaitForSeconds(0.2f);
        ExitState(state);
    }
}
