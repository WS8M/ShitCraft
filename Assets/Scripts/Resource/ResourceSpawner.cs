using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private ResourcePool _resourcePool;
    [SerializeField] private float _spawnSideSize;
    [SerializeField] private float _delayBetweenSpawns;
    [SerializeField] private int _spawnCount;

    private List<Resource> _resources;

    private void OnEnable()
    {
        _resourcePool.Released += DeleteActiveResource;
    }

    private void OnDisable()
    {
        _resourcePool.Released -= DeleteActiveResource;
    }

    private void Awake()
    {
        _resources = new List<Resource>();
        
        _resourcePool.Initialize();
        TryAddResources();
    }

    private IEnumerator SpawnWithDelay(int numberOfSpawns)
    {
        float counter = _resources.Count;
        
        var wait = new WaitForSeconds(_delayBetweenSpawns);
        while (enabled && counter < numberOfSpawns)
        {
            var resource = _resourcePool.GetObject(GetSpawnPosition() , Quaternion.identity);
            _resources.Add(resource);
            counter++;
            
            yield return wait;
        }
    }

    private void TryAddResources()
    {
        if (_resources.Count == 0) 
            StartCoroutine(SpawnWithDelay(_spawnCount));
    }

    private Vector3 GetSpawnPosition()
    {
        var x = Random.Range(-_spawnSideSize, _spawnSideSize);
        var z = Random.Range(-_spawnSideSize, _spawnSideSize);
        
        return transform.position + new Vector3(x, 0, z);
    }

    private void DeleteActiveResource(Resource resource)
    {
        _resources.Remove(resource);
        TryAddResources();
    }
}