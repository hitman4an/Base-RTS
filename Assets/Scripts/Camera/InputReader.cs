using UnityEngine;

public class InputReader: MonoBehaviour
{
    private const string MouseXAxis = "Mouse X";
    private const string MouseYAxis = "Mouse Y";
    private const string Vertical = "Vertical";
    private const string Horizontal = "Horizontal";

    public float GetMouseXAxis()
    {
        return Input.GetAxis(MouseXAxis);
    }

    public float GetMouseYAxis()
    {
        return Input.GetAxis(MouseYAxis);
    }

    public float GetVertical()
    {
        return Input.GetAxis(Vertical);
    }

    public float GetHorizontal()
    {
        return Input.GetAxis(Horizontal);
    }
}
