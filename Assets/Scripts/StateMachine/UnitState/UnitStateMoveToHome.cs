public class UnitStateMoveToHome : UnitState, IState
{
    public UnitStateMoveToHome(Unit unit, Mover mover, UnitStateMachine unitStateMachine) :
        base(unit, mover, unitStateMachine) { }

    public void Enter()
    {
        Mover.Move(Unit.BasePosition, _ => OnCompleteMoving());
    }

    private void OnCompleteMoving()
    {
        if(Unit.IsBuilder)
            StateSwithcer.Enter<UnitStateIdle>();
        else 
            StateSwithcer.Enter<UnitStateGivingAwayResource>();
    }
}