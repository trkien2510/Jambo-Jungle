using UnityEngine;

public class TurretStateManager : Subject<SoundEvent>
{
    State<TurretStateManager> currentState;

    public TurretNomalState nomalState = new TurretNomalState();
    public TurretBrokeState brokeState = new TurretBrokeState();

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentState = nomalState;
        currentState.EnterState(this);

        foreach (var observer in FindObjectsOfType<MonoBehaviour>())
        {
            if (observer is IObserver<SoundEvent> obs)
            {
                AddObserver(obs);
            }
        }
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchBrokeState()
    {
        NotifyObserver(SoundEvent.explosion);
        currentState = brokeState;
        currentState.EnterState(this);
        GetComponent<TurretHealth>().enabled = false;
        GetComponent<TurretSpawnBullet>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public Animator anim => animator;
}