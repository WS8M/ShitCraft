using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private Base _startBase;
    
    private void Awake()
    {
        new ResourceRegistrar();
        
        _startBase.Initialize();
        _startBase.CreateStartUnits();
    }
}