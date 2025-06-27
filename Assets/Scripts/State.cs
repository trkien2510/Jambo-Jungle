public abstract class State<T>
{
    public abstract void EnterState(T state);
    public abstract void UpdateState(T state);
    public abstract void ExitState(T state);
}
