using UnityEngine;
using System;

[RequireComponent(typeof(UnitAnimator))]
public class UnitMover : MonoBehaviour
{
    public event Action TargetReached;

    [SerializeField] private float _speed = 5;

    private UnitAnimator _animator;
    private Vector3 _target;
    private bool _isMoving;

    private void Awake()
    {
        _animator = GetComponent<UnitAnimator>();
    }

    private void Update()
    {
        if (_isMoving)
        {
            Move();
        }
    }

    public void MoveToTarget(Vector3 target)
    {
        _target = target;
        _isMoving = true;
        _animator.SetSpeed(_speed);
    }

    private void Move()
    {
        transform.LookAt(_target);
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);

        if (transform.position == _target) 
        {
            _animator.SetSpeed(0);
            _isMoving = false;
            TargetReached?.Invoke();
        }
    }
}
