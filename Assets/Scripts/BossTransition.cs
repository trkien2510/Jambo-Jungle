using Cinemachine;
using UnityEngine;

public class BossTransition : Subject
{
    [SerializeField] PolygonCollider2D boundBossRoom;
    [SerializeField] CinemachineConfiner2D confiner;
    [SerializeField] Transform newLocation;
    [SerializeField] Transform newTarget;

    void Start()
    {
        confiner = FindObjectOfType<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            confiner.m_BoundingShape2D = boundBossRoom;
            confiner.gameObject.GetComponent<CinemachineVirtualCamera>().Follow = newTarget;

            collision.gameObject.transform.position = newLocation.position;
            NotifyObserver(GameEvent.BossTheme);
            NotifyObserver(GameEvent.BossIntro);
        }
    }
}
