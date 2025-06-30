using System.Collections;
using UnityEngine;

public class BossLaserState : State<BossStateManager>
{
    [SerializeField] private float laserSpeed = 20f;

    private GameObject leftLaserHead, rightLaserHead;
    private GameObject leftLaserBody, rightLaserBody;

    public override void EnterState(BossStateManager state)
    {
        state.anim.SetBool("Laser", true);
        state.LeftEye.GetComponent<Animator>().SetBool("Charge", true);
        state.RightEye.GetComponent<Animator>().SetBool("Charge", true);
        state.StartCoroutine(FireLaser(state));
    }

    public override void ExitState(BossStateManager state)
    {
        state.anim.SetBool("Laser", false);
    }

    public override void UpdateState(BossStateManager state)
    {
    }

    private IEnumerator FireLaser(BossStateManager state)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) yield break;

        Vector3 playerPos = player.transform.position;

        yield return new WaitForSeconds(1f);

        state.NotifyBossState(SoundEvent.laser);

        state.LeftEye.GetComponent<Animator>().SetBool("Charge", false);
        state.RightEye.GetComponent<Animator>().SetBool("Charge", false);

        Vector3 leftEyePos = state.LeftEye.transform.position;
        Vector3 rightEyePos = state.RightEye.transform.position;

        Vector2 dirLeft = (playerPos - leftEyePos).normalized;
        Vector2 dirRight = (playerPos - rightEyePos).normalized;

        leftLaserHead = ObjectPooler.Instance.SpawnObject("BossBullet", leftEyePos, Quaternion.identity);
        var leftBullet = leftLaserHead.GetComponent<EnemyBullet>();
        leftBullet.SetSpeed(laserSpeed);
        leftBullet.Initialize(dirLeft);

        leftLaserBody = Object.Instantiate(state.LaserBody, leftEyePos, Quaternion.identity);
        state.StartCoroutine(UpdateLaserBodyFollowHead(leftLaserBody, leftEyePos, leftLaserHead.transform));

        rightLaserHead = ObjectPooler.Instance.SpawnObject("BossBullet", rightEyePos, Quaternion.identity);
        var rightBullet = rightLaserHead.GetComponent<EnemyBullet>();
        rightBullet.SetSpeed(laserSpeed);
        rightBullet.Initialize(dirLeft);

        rightLaserBody = Object.Instantiate(state.LaserBody, rightEyePos, Quaternion.identity);
        state.StartCoroutine(UpdateLaserBodyFollowHead(rightLaserBody, rightEyePos, rightLaserHead.transform));

        yield return new WaitForSeconds(2f);

        leftBullet.SetSpeed(10f);
        rightBullet.SetSpeed(10f);

        ExitState(state);
        state.SwitchBossState(state.bossIdleState);
    }


    private IEnumerator UpdateLaserBodyFollowHead(GameObject laserBody, Vector3 startPoint, Transform laserHead)
    {
        float originalLength = 1f;

        while (laserHead != null && laserHead.gameObject.activeInHierarchy)
        {
            Vector3 direction = (laserHead.position - startPoint);
            float distance = direction.magnitude;

            laserBody.transform.position = startPoint + direction / 2f;

            laserBody.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);

            laserBody.transform.localScale = new Vector3(
                1f,
                (distance / originalLength) * 8,
                1f
            );

            yield return null;
        }

        if (laserBody) Object.Destroy(laserBody);
    }

}
