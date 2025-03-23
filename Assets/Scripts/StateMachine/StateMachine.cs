using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine<T> : IStateSwitcher where T  :class, IExitableState
{
    protected Dictionary<Type,T> States;
    
    public T ActiveState { get; private set; }
    
    public void Enter<TState>() where TState : class, IState
    {
        IState state = ChangeState<TState>();
        state.Enter();
    }
    
    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
        TState state = ChangeState<TState>();
        state.Enter(payload);  
    }
    
    private TState ChangeState<TState>() where TState : class, IExitableState
    {
        ActiveState?.Exit();
        TState state = GetState<TState>();
        ActiveState = state as T;
        return state;
    }
    
    private TState GetState<TState>() where TState : class, IExitableState
    {
        return States[typeof(TState)] as TState;
    }
}