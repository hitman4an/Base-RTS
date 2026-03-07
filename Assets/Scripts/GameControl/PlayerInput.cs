using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string MouseWheel = "Mouse ScrollWheel";
    private const int RotationButton = 2;
    private const int SelectButton = 0;
    private const int ActionButton = 1;

    public event Action MovingButtonPressed;
    public event Action<Vector3> RotationButtonPressed;
    public event Action<float> MouseWheelChanged;
    public event Action ActionButtonPressed;
    public event Action SelectButtonPressed;

    public void GetInput()
    {
        if (Input.GetButton(Horizontal) || Input.GetButton(Vertical))
            MovingButtonPressed?.Invoke();
        
        if (Input.GetMouseButton(RotationButton))
        {
            Vector3 targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            RotationButtonPressed?.Invoke(targetPoint);
        }

        if (Input.GetAxis(MouseWheel) != 0)
            MouseWheelChanged?.Invoke(Input.GetAxis(MouseWheel));

        if (Input.GetMouseButtonUp(SelectButton))
        {
            SelectButtonPressed?.Invoke();
        }

        if (Input.GetMouseButtonUp(ActionButton))
        {
            ActionButtonPressed?.Invoke();
        }
    }
}
