using UnityEngine;

public class PlayerInput : MonoBehaviour
{ 
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    
    private bool _readyToCLear;
    
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }
    
    private void Update()
    {
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
}