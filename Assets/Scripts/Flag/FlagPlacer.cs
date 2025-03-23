using System;
using UnityEngine;

public class FlagPlacer : MonoBehaviour
{
    private const int MinimalUnitsToPlaceFlag = 2;
    
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private FlagPool _flagPool;
    
    private Base _base;

    public event Action<Vector3> BaseSelected;
    public event Action BaseDeselected;

    private void OnEnable()
    {
        _playerInput.LeftClick += OnLeftClick;
        _playerInput.RightClick += OnRightClick;
    }

    private void OnDisable()
    {
        _playerInput.LeftClick -= OnLeftClick;
        _playerInput.RightClick -= OnRightClick;
    }

    private void Awake()
    {
        _flagPool.Initialize();
    }

    private void OnLeftClick(RaycastHit hit)
    {
        if (_base is null)
        {
            SelectBase(hit);
        }
        else
        {
            if (_base.NumberOfUnits >= MinimalUnitsToPlaceFlag)
                PlaceFlag(hit);
        }
    }

    private void OnRightClick(RaycastHit hit)
    {
        _base = null;
        BaseDeselected?.Invoke();        
    }

    private void SelectBase(RaycastHit hit)
    {
        Debug.LogWarning(hit.collider.gameObject.name);
        if (hit.collider.TryGetComponent(out Base @base))
        {
            _base = @base;
            BaseSelected?.Invoke(_base.gameObject.transform.position);
        }
    }


    private void PlaceFlag(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out Ground ground) == false) 
            return;
        
        if (_base.ActiveState is not BaseStateCreateNewBase)
        {
            Flag flag = _flagPool.GetObject(hit.point);
            _base.AddFlag(flag);
            _base.EnableMakeBaseState();
        }
        else
        {
            _base.Flag.transform.position = hit.point;
        }
    }
}