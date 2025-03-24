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
        _storage.Changed += UpdateView;
    }
    
    private void OnDisable()
    {
        _storage.Changed -= UpdateView;
    }

    private void UpdateView()
    {
        const string defaultValue = "0";
        var text = string.Empty;
        bool isTextSet = false;
        
        for (var index = 0; index < _storage.Cells.Count; index++)
        {
            var cell = _storage.Cells[index];
            text += $"{cell.Value}";
            isTextSet = true;
        }

        if (isTextSet == false)
        {
            text = defaultValue;
        }

        _textField.text = text;
    }
}
