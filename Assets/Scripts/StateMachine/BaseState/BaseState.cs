public abstract class BaseState : IExitableState
{
    protected Base Base;
    protected Storage Storage;
    protected readonly IStateSwitcher StateSwithcer;

    public BaseState(Storage storage, Base @base, IStateSwitcher stateSwitcher)
    {
        StateSwithcer = stateSwitcher;
        Base = @base;
        Storage = storage;
    }

    public virtual void Exit()
    {
    }
}