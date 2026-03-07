using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private float _speed = 300f;

    private InputReader _reader;

    private void Awake()
    {
        _reader = GetComponent<InputReader>();
    }

    public void Rotate(Vector3 targetPoint)
    {
        float mouseX = _reader.GetMouseXAxis() * _speed * Time.deltaTime;
        float mouseY = _reader.GetMouseYAxis() * _speed * Time.deltaTime;

        transform.RotateAround(targetPoint, Vector3.up, mouseX);
        transform.RotateAround(targetPoint, transform.right, mouseY);
    }
}
