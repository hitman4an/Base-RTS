using UnityEngine;

[RequireComponent(typeof(CameraMover))]
[RequireComponent(typeof(CameraZoomer))]
[RequireComponent(typeof(CameraRotator))]
public class GameCamera : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    private CameraMover _mover;
    private CameraZoomer _zoomer;
    private CameraRotator _rotator;
            
    private void Awake()
    {
        _mover = GetComponent<CameraMover>();
        _zoomer = GetComponent<CameraZoomer>();
        _rotator = GetComponent<CameraRotator>();        
    }

    private void OnEnable()
    {
        _playerInput.MovingButtonPressed += Move;
        _playerInput.RotationButtonPressed += Rotate;
        _playerInput.MouseWheelChanged += Zoom;
    }

    private void OnDisable()
    {
        _playerInput.MovingButtonPressed -= Move;
        _playerInput.RotationButtonPressed -= Rotate;
        _playerInput.MouseWheelChanged -= Zoom;
    }

    private void LateUpdate()
    {
        _playerInput.GetInput();
    }

    private void Move()
    {
        _mover.Move();
    }

    private void Zoom(float scrollValue)
    {
        _zoomer.Zoom(scrollValue);
    }

    private void Rotate(Vector3 targetPoint)
    {
        _rotator.Rotate(targetPoint);
    }
}
