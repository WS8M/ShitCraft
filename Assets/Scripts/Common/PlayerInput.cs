using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{ 
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    
    [SerializeField] private Camera _camera;
    private bool _isReadyToCLear;

    public event Action<RaycastHit> ClickedInteractionButton;
    public event Action<RaycastHit> ClickedCancelButton;
    
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
            ClickInteractionButton();
        
        if(Input.GetKeyDown(KeyCode.Mouse1))
            ClickCancelButton();
        
        if (_isReadyToCLear) 
            ClearInputs();

        ProcessInputs();
    }

    private void FixedUpdate()
    {
        _isReadyToCLear = true;
    }
    
    private void ProcessInputs()
    {
        HorizontalInput = Input.GetAxis(Horizontal);
        VerticalInput = Input.GetAxis(Vertical);
    }

    private void ClearInputs()
    {
        HorizontalInput = 0;
        VerticalInput = 0;
        
        _isReadyToCLear = false;
    }

    private void ClickInteractionButton()
    {
        Ray castPoint = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(castPoint, out RaycastHit hit))
            ClickedInteractionButton?.Invoke(hit);
    }
    
    private void ClickCancelButton()
    {
        Ray castPoint = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(castPoint, out RaycastHit hit))
        {
            ClickedCancelButton?.Invoke(hit);
        }
    }
}