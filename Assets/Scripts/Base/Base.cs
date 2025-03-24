using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private UnitAdministrator _unitAdministrator;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private ConstructionCostConfig _constructionCostConfig;
    [SerializeField] private StorageView _storageView;
    
    private ResourceRegistrar _resourceRegistrar;
    private Storage _storage;
    private BaseStateMachine _baseStatemachine;
    private Flag _flag;

    public IReadonlyStorage Storage => _storage;
    public ConstructionCostConfig ConstructionCostConfig => _constructionCostConfig;
    public BaseState ActiveState => _baseStatemachine.ActiveState;
    public int NumberOfUnits => _unitAdministrator.NumberOfUnits;
    public Flag Flag => _flag;
    
    private void OnEnable()
    {
        _unitAdministrator.UnitBroughtResource += CollectResource;
    }

    private void OnDisable()
    {
        _unitAdministrator.UnitBroughtResource -= CollectResource;
    }
    
    public void Initialize()
    {
        _storage = new Storage();
        _scanner.StartScanning(AddingResource);
        _storageView.Initialize();
        _resourceRegistrar = ResourceRegistrar.Instance;
        
        _baseStatemachine = new BaseStateMachine(this, _storage, _unitAdministrator);
        _baseStatemachine.Enter<BaseStateCreateUnit>();
    }

    public void AddFlag(Flag flag)
    {
        if(flag == null)
            return;
        
        _flag = flag;
    }

    public void EnableMakeBaseState()
    {
        _baseStatemachine.Enter<BaseStateCreateNewBase>();
    }

    public void CreateStartUnits(int numberOfUnits = 1)
    {
        _unitAdministrator.InitializeStartUnits(numberOfUnits);
    }

    public void AddUnit(Unit unit)
    {
        _unitAdministrator.AddUnit(unit);
    }

    private void CollectResource(Unit unit)
    {
        var resource = unit.GiveResource();
        
        _storage.AddItem(resource.Collect());
    }

    private void AddingResource(List<Resource> resources)
    {
        foreach (var resource in resources)
        {
            if (_resourceRegistrar.IsEngaged(resource))
                continue;

            if (_unitAdministrator.TryAddTaskToUnit(resource))
            {
                _resourceRegistrar.RegisterEngaged(resource);
            }
        }
    }
}