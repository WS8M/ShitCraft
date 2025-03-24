using UnityEngine;

public class ResourceFabric: MonoBehaviour
{
    [SerializeField] private Resource _prefabs;
    
    private int _spawnPrefabIndex;

    public Resource Create()
    {
        var resource = Instantiate(_prefabs);
        return resource;
    }
}