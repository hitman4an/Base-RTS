using UnityEngine;

[RequireComponent(typeof(CameraMover))]
[RequireComponent(typeof(CameraZoomer))]
[RequireComponent(typeof(CameraRotator))]
[RequireComponent(typeof(PlayerInput))]
public class GameCamera : MonoBehaviour
{
    private CameraMover _mover;
    private CameraZoomer _zoomer;
    private CameraRotator _rotator;
    private PlayerInput _playerInput;
        
    private void Awake()
    {
        _mover = GetComponent<CameraMover>();
        _zoomer = GetComponent<CameraZoomer>();
        _rotator = GetComponent<CameraRotator>();
        _playerInput = GetComponent<PlayerInput>();
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
