public class UnitStateIdle : UnitState, IState
{
    public UnitStateIdle(Unit unit, Mover mover, IStateSwitcher stateSwitcher) : base(unit, mover, stateSwitcher)
    {
    }

    public void Enter()
    {
        Unit.GotTask += EnterToMoveState;
    }

    public override void Exit()
    {
        Unit.GotTask -= EnterToMoveState;
    }


    private void EnterToMoveState()
    {
        StateSwithcer.Enter<UnitStateMoveToResource>();
    }
}