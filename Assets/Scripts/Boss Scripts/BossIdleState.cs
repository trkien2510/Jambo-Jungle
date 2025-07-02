using UnityEngine;

public class BossIdleState : State<BossStateManager>
{
    private float timeInterval = 1.5f;
    private float timer = 0f;
    public override void EnterState(BossStateManager state)
    {
        timer = 0f;
    }

    public override void ExitState(BossStateManager state)
    {

    }

    public override void UpdateState(BossStateManager state)
    {
        timer += Time.deltaTime;
        if (timer >= timeInterval)
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                state.SwitchBossState(state.bossBulletState);
            }
            else
            {
                state.SwitchBossState(state.bossLaserState);
            }
            timer = 0f;
        }
    }
}
