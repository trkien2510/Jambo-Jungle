using UnityEngine;

public class TurretStateManager : MonoBehaviour
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
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchCurrentState(State<TurretStateManager> state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public Animator anim => animator;
}