using UnityEngine;

public class EnemyShootState : State<EnemyStateManager>
{
    private Vector2 direction;
    private float shootInterval = 1.5f;
    private float timer = 0f;

    public override void EnterState(EnemyStateManager state)
    {
        state.anim.SetBool("Shoot", true);
        state.anim.SetBool("Run", false);
        timer = 0f;
    }

    public override void ExitState(EnemyStateManager state)
    {
        state.anim.SetBool("Shoot", false);
    }

    public override void UpdateState(EnemyStateManager state)
    {
        timer += Time.deltaTime;

        Vector3 playerPos = state.Player.position;
        Vector3 enemyPos = state.transform.position;
        float xDelta = playerPos.x - enemyPos.x;
        float yDelta = Mathf.Abs(playerPos.y - enemyPos.y);

        if (xDelta > 0 && !state.IsFacingRight)
            state.Flip();
        else if (xDelta < 0 && state.IsFacingRight)
            state.Flip();

        if (timer >= shootInterval && yDelta < 2f)
        {
            direction = state.IsFacingRight ? Vector2.right : Vector2.left;
            SpawnBullet("EnemyBullet", state);
            timer = 0f;
        }

        if (!state.CanSeePlayer())
        {
            state.SwitchEnemyState(state.enemyIdleState);
        }
    }

    private void SpawnBullet(string tag, EnemyStateManager state)
    {
        GameObject enemyBullet = ObjectPooler.Instance.SpawnObject(tag, state.transform.position, Quaternion.identity);
        if (enemyBullet != null)
            enemyBullet.GetComponent<EnemyBullet>().Initialize(direction);
    }
}
