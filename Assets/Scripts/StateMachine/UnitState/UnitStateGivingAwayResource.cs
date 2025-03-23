public class UnitStateGivingAwayResource : UnitState, IState
{
    public UnitStateGivingAwayResource(Unit unit, Mover mover, IStateSwitcher stateSwitcher) : base(unit, mover, stateSwitcher)
    {
    }

    public void Enter()
    {
        Unit.InvokeBringResourceEvent();
    }

    public override void Update()
    {
        if(Unit.IsHaveResource)
            return;
        
        StateSwithcer.Enter<UnitStateIdle>();
    }
}