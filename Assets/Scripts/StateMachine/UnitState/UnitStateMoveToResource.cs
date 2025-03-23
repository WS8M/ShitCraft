public class UnitStateMoveToResource: UnitState, IState
{
    public UnitStateMoveToResource(Unit unit, Mover mover, IStateSwitcher stateSwitcher) : 
        base(unit, mover, stateSwitcher) { }

    public void Enter()
    {
        Mover.Move(Unit.TargetPosition, _ => StateSwithcer.Enter<UnitStateCollectResource>());
    }
}