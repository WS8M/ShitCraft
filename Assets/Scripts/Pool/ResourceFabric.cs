using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ResourceFabric: MonoBehaviour
{
    private static int s_currentId;

    [SerializeField] private List<Resource> _prefabs;
    
    private int _spawnPrefabIndex = 0;

    public Resource Create()
    {
        var resource = Instantiate(_prefabs[_spawnPrefabIndex]);
        resource.Initialize(s_currentId);
        
        s_currentId++;
        _spawnPrefabIndex = ++_spawnPrefabIndex % _prefabs.Count;
        
        return resource;
    }
}