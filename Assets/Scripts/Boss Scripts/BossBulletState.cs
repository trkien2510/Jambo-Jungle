using System.Collections;
using UnityEngine;

public class BossBulletState : State<BossStateManager>
{
    public override void EnterState(BossStateManager state)
    {
        state.anim.SetBool("Bullet", true);
        state.StartCoroutine(WaitToSpawnBullet(state));

        state.NotifyBossState(SoundEvent.bossFire);
    }

    public override void ExitState(BossStateManager state)
    {
        state.anim.SetBool("Bullet", false);
    }

    public override void UpdateState(BossStateManager state)
    {
        
    }

    IEnumerator WaitToSpawnBullet(BossStateManager state)
    {
        yield return new WaitForSeconds(1f);
        state.Mouth.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(1f);
        state.Mouth.GetComponent<SpriteRenderer>().enabled = false;
        SpawnBullets(state);
        ExitState(state);
        state.SwitchBossState(state.bossIdleState);
    }

    private void SpawnBullets(BossStateManager state)
    {
        int bulletCount = 16;
        float angleStep = 360f / bulletCount;
        float startAngle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + i * angleStep;
            float rad = angle * Mathf.Deg2Rad;

            Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            GameObject bullet = ObjectPooler.Instance.SpawnObject("BossBullet", state.Mouth.transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().Initialize(direction);
        }
    }
}
