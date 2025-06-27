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
            Vector2 dir = (player.transform.position - state.transform.position);

            if (dir.magnitude < 0.1f) return;

            int x = Mathf.RoundToInt(dir.normalized.x);
            int y = Mathf.RoundToInt(dir.normalized.y);

            state.anim.SetFloat("DirX", x);
            state.anim.SetFloat("DirY", y);
        }
    }

    public override void ExitState(TurretStateManager state)
    {

    }
}
