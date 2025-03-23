using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _speed;

    private void LateUpdate() =>
        Move();

    private void Move()
    {
        Vector3 horizontal = transform.right * _playerInput.HorizontalInput;
        Vector3 vertical = transform.forward * _playerInput.VerticalInput;
        Vector3 direction = horizontal + vertical;
        
        transform.Translate( direction * _speed * Time.deltaTime);
    }
}