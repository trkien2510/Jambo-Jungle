using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    public override void EnterState(PlayerStateManager state)
    {
        state.anim.SetTrigger("Dead");
    }

    public override void UpdateState(PlayerStateManager state)
    {
        
    }

    public override void ExitState(PlayerStateManager state)
    {

    }
}
