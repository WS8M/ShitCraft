using UnityEngine;

public class BaseFabric : MonoBehaviour
{
    public static BaseFabric Instance { get; private set; }
    
    [SerializeField] private Base _prefab;
    [SerializeField] private FlagPlacer _flagPlacer;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning($"Delete duplicate {nameof(BaseFabric)}");
            Destroy(gameObject);
        }

        Instance = this;
    }
    
    public Base Create(Vector3 position)
    {
        var @base = Instantiate(_prefab);
        @base.Initialize();
        @base.transform.position = position;
        return @base;
    }
}