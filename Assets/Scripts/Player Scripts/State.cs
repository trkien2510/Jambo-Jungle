using UnityEngine;

public abstract class State
{
    public abstract void EnterState(PlayerStateManager state);
    public abstract void UpdateState(PlayerStateManager state);
    public abstract void ExitState(PlayerStateManager state);
}
