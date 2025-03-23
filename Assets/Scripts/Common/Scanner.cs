using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField, Range(0, 100f)] private float _radius;
    [SerializeField, Min(0.01f)] private float _delayBetweenScan = 0.5f;

    private Coroutine _scanningCoroutine;
    
    public event Action Scanned;
    
    public float ScanRadius => _radius;

    public void StartScanning(Action<List<Resource>> onScanning)
    {
        if(_scanningCoroutine != null)
            return;
        
        _scanningCoroutine = StartCoroutine(ScanningRoutine(_delayBetweenScan, onScanning));
    }

    private IEnumerator ScanningRoutine(float scannerDelay, Action<List<Resource>> onScanning)
    {
        var wait = new WaitForSeconds(scannerDelay);

        while (enabled)
        {
            yield return wait;
            
            List<Resource> newResources = Scan();
            onScanning(newResources);
        }
    }
    
    private List<Resource> Scan()
    {
        Scanned?.Invoke();
        
        Collider[] hits = Physics.OverlapSphere(transform.position, _radius);
        
        var resources = new List<Resource>();

        foreach (Collider hit in hits)
            if (hit.TryGetComponent(out Resource item)) 
                resources.Add(item);

        return resources;
    }
}