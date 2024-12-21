using System;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField, Range(0, 100f)] private float _radius;

    public event Action OnScan;
    
    public float ScanRadius => _radius;
    
    public List<Resource> Scan()
    {
        OnScan?.Invoke();
        
        Collider[] hits = Physics.OverlapSphere(transform.position, _radius);
        
        var resources = new List<Resource>();

        foreach (Collider hit in hits)
            if (hit.TryGetComponent(out Resource item)) 
                resources.Add(item);

        return resources;
    }
}