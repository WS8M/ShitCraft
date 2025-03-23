using TMPro;
using UnityEngine;

public class StorageView : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private TMP_Text _textField;
    private IReadonlyStorage _storage;

    public void Initialize()
    {
        _storage = _base.Storage;
        _storage.StorageChanged += UpdateView;
    }

    
    private void OnDisable()
    {
        _storage.StorageChanged -= UpdateView;
    }

    private void UpdateView()
    {
        var text = "";
        
        for (var index = 0; index < _storage.Cells.Count; index++)
        {
            var cell = _storage.Cells[index];
            text += $"{cell.Value}";
        }

        _textField.text = text;
    }
}
