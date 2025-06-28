using UnityEngine;

[RequireComponent(typeof(PlayerStateManager))]
public class PlayerSpawnBullet : Subject
{
    private Vector2Int direction;
    private float timeInteval = 0.25f;
    private float timer;
    private PlayerStateManager playerState;

    void Start()
    {
        playerState = GetComponent<PlayerStateManager>();
    }

    void Update()
    {
        Vector2 aimDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

        if (playerState.CurrentState == playerState.crounchState)
        {
            direction = GetTwoDirection(aimDirection);
        }
        else
        {
            direction = GetSixDirection(aimDirection);
        }

        if (!playerState.IsFacingRight && direction.x > 0)
        {
            return;
        }

        if (playerState.IsFacingRight && direction.x < 0)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            SpawnBullet("PlayerBullet");
            timer = 0f;
        }

        if (Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
            if (timer > timeInteval)
            {
                SpawnBullet("PlayerBullet");
                timer = 0f;
            }
        }
    }

    private Vector2Int GetSixDirection(Vector2 dir)
    {
        dir.Normalize();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (angle >= -30 && angle < 30)
            return new Vector2Int(1, 0);
        if (angle >= 30 && angle < 105)
            return new Vector2Int(1, 1);
        if (angle >= 105 && angle < 150)
            return new Vector2Int(-1, 1);
        if (angle >= 150 || angle < -150)
            return new Vector2Int(-1, 0);
        if (angle >= -150 && angle < -105)
            return new Vector2Int(-1, -1);
        if (angle >= -105 && angle < -30)
            return new Vector2Int(1, -1);

        return new Vector2Int(1, 0);
    }

    private Vector2Int GetTwoDirection(Vector2 dir)
    {
        dir.Normalize();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (angle >= -90 && angle < 90)
            return new Vector2Int(1, 0);
        else
            return new Vector2Int(-1, 0);
    }

    private void SpawnBullet(string tag)
    {
        GameObject playerBullet;
        if (playerState.CurrentState == playerState.crounchState)
        {
            playerBullet = ObjectPooler.Instance.SpawnObject(
                tag,
                new Vector2(transform.position.x, transform.position.y - 0.6f),
                Quaternion.identity
                );
        }
        else
        {
            playerBullet = ObjectPooler.Instance.SpawnObject(
                tag,
                new Vector2(transform.position.x, transform.position.y - 0.25f),
                Quaternion.identity
                );
        }
        if (playerBullet != null)
        {
            playerBullet.GetComponent<PlayerBullet>().Initialize(direction);
            NotifyObserver(SoundEvent.shoot);
        }
    }
}
