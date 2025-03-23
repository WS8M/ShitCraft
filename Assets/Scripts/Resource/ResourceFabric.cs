using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ResourceFabric: MonoBehaviour
{
    [SerializeField] private List<Resource> _prefabs;
    
    private int _spawnPrefabIndex;

    public Resource Create()
    {
        var resource = Instantiate(_prefabs[_spawnPrefabIndex]);
        _spawnPrefabIndex = ++_spawnPrefabIndex % _prefabs.Count;

        return resource;
    }
}