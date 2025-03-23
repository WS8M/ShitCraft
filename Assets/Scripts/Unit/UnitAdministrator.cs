using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitAdministrator : MonoBehaviour
{
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private List<Unit> _units;
    
    private Flag _buildingFlag;
    public event Action<Unit> UnitBroughtResource;
    
    public int NumberOfUnits => _units.Count;
    private List<Unit> FreeUnits => _units.FindAll(unit => unit.ActiveState is UnitStateIdle);
    
    public bool TryAddTaskToUnit(ITarget target)
    {
        if (FreeUnits.Count == 0)
            return false;

        Unit unit = FreeUnits.First();

        if (_buildingFlag != null)
        {
            unit.TryTakeTask(_buildingFlag);
            _buildingFlag = null;
            _units.Remove(unit);
            return false;
        }
        
        if (!unit.TryTakeTask(target)) 
            return false;
        
        MakeUnitEngage(unit);
        return true;
    }

    public void SetBuildingFlag(Flag flag)
    {
        if(flag == null)
            return;
        
        _buildingFlag = flag;
    }
    
    public void InitializeStartUnits(int numberOfUnits)
    {
        for(var i = 0; i < numberOfUnits; i++)
            CreateNewUnit();
    }

    public void AddUnit(Unit unit)
    {
        _units.Add(unit);
    }
    
    public void CreateNewUnit()
    {
        var unit = _unitSpawner.Spawn();
        AddUnit(unit);
    }

    private void MakeUnitEngage(Unit unit)
    {
        unit.BringResource += UnitBroughtResource;
        unit.BringResource += MakeUnitFree;
    }

    private void MakeUnitFree(Unit unit)
    {
        unit.BringResource -= UnitBroughtResource;
        unit.BringResource -= MakeUnitFree;
    }
}