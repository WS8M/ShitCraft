public interface IStateSwitcher
{
    public void Enter<TState>() where TState : class, IState;
}