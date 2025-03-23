using System;
using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _movingSpeed;
    [SerializeField] private Unit _unit;
    [SerializeField] private Vector3 _homePosition;
    
    private Vector3 _targetPosition;
    private Coroutine _moveCoroutine;
    
    public void Move(Vector3 targetPosition, Action<Unit> onComplete = null)
    {
        if (_moveCoroutine != null)
            return;
        
        _moveCoroutine = StartCoroutine(Moving(targetPosition, onComplete));
    }

    private IEnumerator Moving(Vector3 targetPosition, Action<Unit> onComplete = null)
    {
        while (transform.position != targetPosition)
        {
            float deltaDistance = _movingSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, deltaDistance);
            transform.LookAt(targetPosition);

            yield return null;
        }

        if (CheckPosition(targetPosition))
        {
            _moveCoroutine = null;
            onComplete?.Invoke(_unit);
        }
    }
    
    private bool CheckPosition(Vector3 targetPosition)
    {
        return transform.position == targetPosition;
    }
}
