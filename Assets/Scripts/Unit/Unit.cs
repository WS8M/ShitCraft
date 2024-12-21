using System;
using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private float _movingSpeed;
    [SerializeField] private GameObject _holdingPosition;

    private Resource _target;
    private bool _isEngaged;
    private bool _isTakedResource;
    
    private Coroutine _moveCoroutine;

    public event Action<Unit> Collected;
    
    public bool IsEngaged => _isEngaged;

    private void Start()
    {
        _isEngaged = false;
        _isTakedResource = false;
    }
    
    public bool TryTakeTask(Resource resource)
    {
        if (_isEngaged)
            return false;

        _target = resource;
        _isEngaged = true;
        
        _moveCoroutine = StartCoroutine(Moving(_target.transform.position));

        return true;
    }

    private IEnumerator Moving(Vector3 target)
    {
        while (transform.position != target)
        {
            float deltaDistance = _movingSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, deltaDistance);
            transform.LookAt(target);

            yield return null;
        }
        CheckPosition();
    }
    
    private void CheckPosition()
    {
        if (transform.position == _target.transform.position && _isTakedResource == false)
        {
            _isTakedResource = true;
            _target.Take(_holdingPosition.transform);
            
            StopCoroutine(_moveCoroutine);
            _moveCoroutine = StartCoroutine(Moving(_base.transform.position));
        }
        else if (transform.position == _base.transform.position && _isTakedResource)
        {
            _base.CollectResource(_target);
            StopCoroutine(_moveCoroutine);
            
            _isTakedResource = false;
            _isEngaged = false;
            
            Collected?.Invoke(this);
        }
    }
}