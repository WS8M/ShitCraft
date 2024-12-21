using System.Collections.Generic;
using UnityEngine;

public class UnitAdministrator : MonoBehaviour
{
    [SerializeField] private List<Unit> _units;
    private Queue<Resource> _tasks = new();

    private List<Unit> _freeUnits => _units.FindAll(unit => unit.IsEngaged == false);

    public bool TryAddTask(Resource resource)
    {
        foreach (var task in _tasks)
            if(task.Id == resource.Id)
                return false;

        _tasks.Enqueue(resource);
        AddTaskForUnit();
        
        return true;
    }

    private void AddTaskForUnit()
    {
        if (_freeUnits.Count == 0)
            return;
        
        foreach (var unit in _freeUnits)
        {
            if (unit.TryTakeTask(_tasks.Peek()))
            {
                MakeUnitEngage(unit);
                _tasks.Dequeue();
                return;
            }
        }
    }

    private void MakeUnitEngage(Unit unit)
    {
        unit.Collected += MakeUnitFree;
    }
    
    private void MakeUnitFree(Unit unit)
    {
        unit.Collected -= MakeUnitFree;
        
        if(_tasks.Count == 0)
            return;
        
        if (unit.TryTakeTask(_tasks.Peek()))
        {
            _tasks.Dequeue();
            MakeUnitEngage(unit);
        }
    }
}