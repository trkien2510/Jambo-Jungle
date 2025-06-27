using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretNomalState : State<TurretStateManager>
{
    public override void EnterState(TurretStateManager state)
    {

    }

    public override void UpdateState(TurretStateManager state)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector2 aimDirection = (player.transform.position - state.transform.position).normalized;

            state.anim.SetFloat("DirX", aimDirection.x);
            state.anim.SetFloat("DirY", aimDirection.y);
        }
    }

    public override void ExitState(TurretStateManager state)
    {

    }
}
