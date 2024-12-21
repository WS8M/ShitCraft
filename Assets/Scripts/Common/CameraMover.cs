using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _speed;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var horizontal = transform.right * _playerInput.HorizontalInput;
        var vertical = transform.forward * _playerInput.VerticalInput;
        transform.Translate( (horizontal + vertical )* _speed * Time.deltaTime);
    }
}
