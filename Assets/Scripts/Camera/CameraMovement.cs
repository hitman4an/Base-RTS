using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _speed = 200f;
    [SerializeField] private float _smoothTime = 0.05f;

    private InputReader _reader;

    private Vector3 _velocity = Vector3.zero;

    private void Awake()
    {
        _reader = GetComponent<InputReader>();
    }

    public void Move()
    {
        float positionZ = _reader.GetVertical() * _speed * Time.deltaTime;
        float positionX = _reader.GetHorizontal() * _speed * Time.deltaTime;

        Vector3 targetPosition = transform.position - new Vector3(positionX, 0, positionZ);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
    }
}
