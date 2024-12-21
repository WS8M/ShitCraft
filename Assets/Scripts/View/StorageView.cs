using System;
using UnityEngine;

public class StorageView : MonoBehaviour
{
    [SerializeField] private Base _base;
    
    private IReadonlyStorage _storage;

    private void Initialize()
    {
        _storage = _base.GetStorage();
        _storage.OnStorageChanged += PrintInfo;
    }

    private void Start()
    {
        Initialize();
    }
    
    private void OnDisable()
    {
        _storage.OnStorageChanged -= PrintInfo;
    }

    private void PrintInfo()
    {
        var text = "\n";
        for (var index = 0; index < _storage.Cells.Count; index++)
        {
            var cell = _storage.Cells[index];
            text += $"{cell.Item.Name} {cell.Value}\t";
        }

        Debug.Log(text);
    }
}
