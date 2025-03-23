using System;
using UnityEngine;

public class FlagPlacerView : MonoBehaviour
{
    [SerializeField] private FlagPlacer _flagPlacer;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private SelectedBaseMarker _selectedMarkerPrefab;
    
    private SelectedBaseMarker _currentSelectedMarker;

    private void OnEnable()
    {
        _flagPlacer.BaseSelected += OnBaseSelected;
        _flagPlacer.BaseDeselected += OnBaseDeselected;
    }

    private void OnDisable()
    {
        _flagPlacer.BaseSelected -= OnBaseSelected;
        _flagPlacer.BaseDeselected -= OnBaseDeselected;
    }

    private void OnBaseDeselected()
    {
        Destroy(_currentSelectedMarker.gameObject);
    }

    private void OnBaseSelected(Vector3 position)
    {
        Vector3 markerPosition = new Vector3(position.x + _offset.x, position.y + _offset.y, position.z + _offset.z);
        _currentSelectedMarker = Instantiate(_selectedMarkerPrefab, markerPosition, Quaternion.identity);
    }
}