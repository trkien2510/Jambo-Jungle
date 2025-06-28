using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBrokeState : State<TurretStateManager>
{
    public override void EnterState(TurretStateManager state)
    {
        state.anim.SetTrigger("Broken");
    }

    public override void UpdateState(TurretStateManager state)
    {

    }

    public override void ExitState(TurretStateManager state)
    {
        
    }
}
