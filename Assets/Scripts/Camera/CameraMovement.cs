using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _speed = 200f;
    [SerializeField] private float _smoothTime = 0.03f;
    
    private Vector3 _velocity = Vector3.zero;    

    public void Move()
    {
        float positionZ = Input.GetAxis("Vertical") * _speed * Time.deltaTime;
        float positionX = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;

        Vector3 targetPosition = transform.position - new Vector3(positionX, 0, positionZ);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
    }
}
