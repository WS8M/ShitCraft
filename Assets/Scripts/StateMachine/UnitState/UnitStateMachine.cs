using System;
using System.Collections.Generic;

public class UnitStateMachine: StateMachine<UnitState>
{
    public UnitStateMachine(Unit unit, Mover mover)
    {
        States = new Dictionary<Type, UnitState>
        {
            [typeof(UnitStateIdle)] = new UnitStateIdle(unit, mover, this),
            [typeof(UnitStateMoveToResource)] = new UnitStateMoveToResource(unit, mover, this),
            [typeof(UnitStateCollectResource)] = new UnitStateCollectResource(unit, mover, this),
            [typeof(UnitStateMoveToHome)] = new UnitStateMoveToHome(unit,mover, this),
            [typeof(UnitStateGivingAwayResource)] = new UnitStateGivingAwayResource(unit, mover, this),
            [typeof(UnitStateBuildNewBase)] = new UnitStateBuildNewBase(unit, mover, this),
        };
    }

    public void Update()
    {
        ActiveState?.Update();
    } 
}