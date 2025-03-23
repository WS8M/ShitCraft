public class UnitStateCollectResource: UnitState , IState
{
    public UnitStateCollectResource(Unit unit,Mover mover, IStateSwitcher stateSwitcher) : base(unit, mover, stateSwitcher)
    {
    }

    public void Enter()
    {
        Unit.HandleCollectResource(() => StateSwithcer.Enter<UnitStateMoveToHome>());
    }
}