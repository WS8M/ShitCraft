using System;
using System.Collections.Generic;

public class BaseStateMachine : StateMachine<BaseState>
{
    public BaseStateMachine(Base @base, Storage storage, UnitAdministrator unitAdministrator)
    {
        States = new Dictionary<Type, BaseState>
        {
            [typeof(BaseStateCreateUnit)] = new BaseStateCreateUnit(storage, @base, unitAdministrator,this),
            [typeof(BaseStateCreateNewBase)] = new BaseStateCreateNewBase(storage, @base, unitAdministrator, this)
        };
    }
}