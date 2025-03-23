using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private GameObject _holdingPosition;

    private bool _isBuilder;
    private Vector3 _basePosition;
    private ITarget _target;
    private UnitStateMachine _stateMachine;

    public void Initialize(Vector3 basePosition)
    {
        _basePosition = basePosition;
        _stateMachine = new UnitStateMachine(this, _mover);
        _stateMachine.Enter<UnitStateIdle>();
        _isBuilder = false;
    }

    public Vector3 BasePosition => _basePosition;
    public Vector3 TargetPosition => _target.Position;
    public ITarget Target => _target;
    public IExitableState ActiveState => _stateMachine.ActiveState;
    public bool IsHaveResource => _target != null;
    public bool IsBuilder => _isBuilder;

    public event Action<Unit> BringResource;
    public event Action GotTask;

    public void Update()
    {
        _stateMachine.Update();
    }
    
    public bool TryTakeTask(ITarget target)
    {
        if (_stateMachine.ActiveState is not UnitStateIdle)
            return false;
        

        if (target as Flag)
        {
            MakeUnitBuilder(target as Flag);
        }
        
        _target = target;
        GotTask?.Invoke();
        return true;
    }

    public Resource GiveResource()
    {
        var givenResource = _target as Resource;
        _target = null;
        return givenResource;
    }

    public void HandleCollectResource(Action onCollectResource)
    {
        if (TryTakeResource(_target as Resource)) 
            onCollectResource?.Invoke();
    }

    public void InvokeBringResourceEvent() => 
        BringResource?.Invoke(this);

    private bool TryTakeResource(Resource resource)
    {
        if(resource == null)
            return false;

        var targetResource = _target as Resource;
        if (targetResource == null)
            return false;
        
        targetResource.Take(_holdingPosition.transform);
        return true;
    }
    
    private void MakeUnitBuilder(ITarget target)
    {
        _isBuilder = true;
        _target = target;
        _stateMachine.Enter<UnitStateBuildNewBase,Vector3>(_target.Position);
    }
}