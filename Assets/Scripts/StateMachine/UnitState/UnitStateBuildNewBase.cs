using UnityEngine;

public class UnitStateBuildNewBase : UnitState, IPayloadedState<Vector3>
{
    public UnitStateBuildNewBase(Unit unit, Mover mover, IStateSwitcher stateSwitcher) : base(unit, mover, stateSwitcher)
    {
    }
    
    public void Enter(Vector3 buildPostion)
    {
        Mover.Move(buildPostion, _ => BuildNewBase());
    }

    private void BuildNewBase()
    {
        if (Unit.Target is Flag flag)
        {
           Base @base = BaseFabric.Instance.Create(flag.Position);
           @base.AddUnit(Unit);
           Unit.Initialize(@base.gameObject.transform.position);
           
           flag.DeleteFlag();
        }
    }
}