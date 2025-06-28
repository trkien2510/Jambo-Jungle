using UnityEngine;

public class EnemyRunState : State<EnemyStateManager>
{
    private float moveSpeed = 2f;
    public override void EnterState(EnemyStateManager state)
    {
        state.anim.SetBool("Run", true);
        state.anim.SetBool("Shoot", false);
    }

    public override void ExitState(EnemyStateManager state)
    {
        state.anim.SetBool("Run", false);
    }

    public override void UpdateState(EnemyStateManager state)
    {
        if (state.CanSeePlayer())
        {
            state.SwitchEnemyState(state.enemyShootState);
            return;
        }

        Vector3 pos = state.transform.position;
        float targetX = state.IsFacingRight ? state.RightPoint.x : state.LeftPoint.x;
        Vector3 target = new Vector3(targetX, pos.y, pos.z);

        state.transform.position = Vector3.MoveTowards(pos, target, moveSpeed * Time.deltaTime);

        if (Mathf.Abs(pos.x - targetX) < 0.05f)
        {
            state.transform.position = target;
            state.Flip();
            state.SwitchEnemyState(state.enemyIdleState);
        }
    }
}
