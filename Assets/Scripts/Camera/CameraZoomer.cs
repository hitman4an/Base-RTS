using UnityEngine;

public class CameraZoomer : MonoBehaviour
{
    [SerializeField] private float _speed = 500f; 
    [SerializeField] private float _smoothTime = 0.001f;

    private float _minZoom = 5f;
    private float _maxZoom = 20f;
    private Vector3 _velocity = Vector3.zero;

    public void Zoom(float scrollValue)
    {
        Vector3 position = transform.position;

        position.y -= scrollValue * _speed * Time.deltaTime;
        position.y = Mathf.Clamp(position.y, _minZoom, _maxZoom);

        transform.position = Vector3.SmoothDamp(transform.position, position, ref _velocity, _smoothTime);            
    }
}
