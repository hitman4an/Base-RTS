using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    private const string MouseXAxis = "Mouse X";
    private const string MouseYAxis = "Mouse Y";

    [SerializeField] private float _speed = 300f;

    public void Rotate(Vector3 targetPoint)
    {
        float mouseX = Input.GetAxis(MouseXAxis) * _speed * Time.deltaTime;
        float mouseY = Input.GetAxis(MouseYAxis) * _speed * Time.deltaTime;

        transform.RotateAround(targetPoint, Vector3.up, mouseX);
        transform.RotateAround(targetPoint, transform.right, mouseY);
    }
}
