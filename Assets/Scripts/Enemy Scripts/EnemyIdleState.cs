using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : State<EnemyStateManager>
{
    private float idleTime = 2f;
    private float timer = 0f;

    public override void EnterState(EnemyStateManager state)
    {
        state.anim.SetBool("Run", false);
        state.anim.SetBool("Shoot", false);
        timer = 0f;
    }

    public override void ExitState(EnemyStateManager state)
    {
        
    }

    public override void UpdateState(EnemyStateManager state)
    {
        timer += Time.deltaTime;

        if (state.CanSeePlayer())
        {
            state.SwitchEnemyState(state.enemyShootState);
            return;
        }

        if (timer >= idleTime)
        {
            if (Random.value < 0.5f)
                state.SwitchEnemyState(state.enemyRunState);
            else
                timer = 0f;
        }
    }
}
