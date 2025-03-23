using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{ 
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    
    [SerializeField] private Camera _camera;
    private bool _readyToCLear;

    public event Action<RaycastHit> LeftClick;
    public event Action<RaycastHit> RightClick;
    
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
            LeftClickAction();
        
        if(Input.GetKeyDown(KeyCode.Mouse1))
            RightClickAction();
        
        if (_readyToCLear) 
            ClearInputs();

        ProcessInputs();
    }

    private void FixedUpdate()
    {
        _readyToCLear = true;
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
        
        _readyToCLear = false;
    }

    public void LeftClickAction()
    {
        Ray castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(castPoint, out RaycastHit hit))
            LeftClick?.Invoke(hit);
    }
    
    private void RightClickAction()
    {
        Ray castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(castPoint, out RaycastHit hit))
        {
            RightClick?.Invoke(hit);
        }
    }
}