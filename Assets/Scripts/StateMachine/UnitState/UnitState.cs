public abstract class UnitState : IExitableState
{ 
    protected readonly Unit Unit;
    protected readonly Mover Mover;
    protected readonly IStateSwitcher StateSwithcer;

    public UnitState(Unit unit, Mover mover, IStateSwitcher stateSwitcher)
    {
        Unit = unit;
        Mover = mover;
        StateSwithcer = stateSwitcher;
    }

    public virtual void Update(){}

    public virtual void Exit()
    {
    }
}