using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private UnitFabric _unitFabric;
    [SerializeField] private Base _base;

    public Unit Spawn()
    {
        var unit = _unitFabric.Create(_base.transform.position);
        unit.transform.position = _spawnPoint.position;
        
        return unit;
    }
}
