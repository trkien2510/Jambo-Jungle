using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class BossTransition : Subject<SoundEvent>
{
    [SerializeField] PolygonCollider2D boundBossRoom;
    [SerializeField] CinemachineConfiner2D confiner;
    [SerializeField] PlayableDirector bossIntro;
    [SerializeField] Transform newLocation;

    void Start()
    {
        confiner = FindObjectOfType<CinemachineConfiner2D>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            confiner.m_BoundingShape2D = boundBossRoom;

            collision.gameObject.transform.position = newLocation.position;
            bossIntro.Play();
            NotifyObserver(SoundEvent.bossTheme);
        }
    }
}
