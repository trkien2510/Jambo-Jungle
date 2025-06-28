using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private float spawnInterval = 3f;
    private float spawnTimer = 0f;
    private Transform player;
    private float detectRange = 5f;

    void Start()
    {
        
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (CanSeePlayer())
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                PortalSpawnEnemy("Enemy");
                spawnTimer = 0f;
            }
        }
    }

    public bool CanSeePlayer()
    {
        return Vector3.Distance(transform.position, player.position) <= detectRange;
    }

    private void PortalSpawnEnemy(string tag)
    {
        GameObject enemy = ObjectPooler.Instance.SpawnObject(tag, transform.position, Quaternion.identity);
        if (enemy != null)
            enemy.GetComponent<EnemyStateManager>().Initialize();
    }
}
