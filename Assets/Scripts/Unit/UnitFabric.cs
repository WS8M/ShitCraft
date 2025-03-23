using UnityEngine;

public class UnitFabric : MonoBehaviour
{
    [SerializeField] private Unit _prefab;
    
    public Unit Create(Vector3 basePosition)
    {
        var unit = Instantiate(_prefab);
        unit.Initialize(basePosition);
        return unit;
    }
}