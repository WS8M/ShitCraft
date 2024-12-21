using UnityEngine;

public class ScannerView : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;
    [SerializeField] private ParticleSystem _scannerParticles;

    [SerializeField] private float _duration;

    private void OnEnable()
    {
        var main = _scannerParticles.main;
        main.startLifetime = _duration;
        main.startSize = _scanner.ScanRadius;
        
        _scanner.OnScan += PlayEffects;
    }

    private void OnDisable()
    {
        _scanner.OnScan -= PlayEffects;
    }

    private void PlayEffects()
    {
        _scannerParticles.Play();
    }
}
