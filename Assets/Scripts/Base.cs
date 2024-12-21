using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private UnitAdministrator _unitAdministrator;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private float _scannerDelay;
    
    private Storage _storage;
    
    private List<Resource> _engageResources;
    
    private void Awake()
    {
        _storage = new Storage();
        _engageResources = new List<Resource>();
        StartCoroutine(ScanningRoutine());
    }

    private IEnumerator ScanningRoutine()
    {
        var wait = new WaitForSeconds(_scannerDelay);

        while (enabled)
        {
            yield return wait;
            
            List<Resource> newResources = _scanner.Scan();
            AddingResource(newResources);
        }
    }

    private void AddingResource(List<Resource> resources)
    {
        foreach (var resource in resources)
        {
            if(_engageResources.Any(engageResource => engageResource.Id == resource.Id))
                continue;
            
            if(_unitAdministrator.TryAddTask(resource))
                _engageResources.Add(resource);
        }
    }

    public void CollectResource(Resource resource)
    {
        RemoveResource(resource);
        
        _storage.AddItem(resource.Collect());
    }
    private void RemoveResource(Resource resource)
    {
        _engageResources.Remove(resource);
    }

    public IReadonlyStorage GetStorage()
    {
        return _storage;
    }
}