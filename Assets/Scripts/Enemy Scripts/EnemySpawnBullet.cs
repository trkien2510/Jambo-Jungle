using UnityEngine;

public class EnemySpawnBullet : MonoBehaviour
{
    private Vector2Int direction;
    private float timeInteval = 2f;
    private float timer;

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector2 aimDirection = (player.transform.position - transform.position).normalized;
            direction = new Vector2Int(
                Mathf.RoundToInt(aimDirection.x),
                Mathf.RoundToInt(aimDirection.y)
            );
        }
        timer += Time.deltaTime;
        if (timer >= timeInteval)
        {
            SpawnEnemyBullet("EnemyBullet");
            timer = 0;
        }
    }

    private void SpawnEnemyBullet(string tag)
    {
        GameObject enmeyBullet = ObjectPooler.Instance.SpawnObject(tag, transform.position, Quaternion.identity);
        if (enmeyBullet != null)
        {
            enmeyBullet.GetComponent<EnemyBullet>().Initialize(direction);
        }
    }
}
