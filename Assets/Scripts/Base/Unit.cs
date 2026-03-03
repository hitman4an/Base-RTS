using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
public class Unit : MonoBehaviour
{
    [SerializeField] private float _speed = 5;

    public event Action ResourceBrought;

    private Base _base;
    private Animator _animator;

    private Vector3 _target = Vector3.zero;

    public bool IsFree { get; private set; } = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        MoveToTarget();
    }

    public void GetResource(Vector3 target)
    {
        IsFree = false;

        _target = target;
    }

    public void SetBase(Base unitBase)
    {
        _base = unitBase;
    }

    private void MoveToTarget()
    {
        if (_target != Vector3.zero) 
        {
            transform.LookAt(_target);
            transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
            _animator.SetFloat("Speed", _speed);
        }
        else
        {
            _animator.SetFloat("Speed", 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.position == _target)
        {
            if (other.gameObject.GetComponent<Resource>())
            {
                TakeResource(other.gameObject);
                _target = _base.transform.position;
            }
            else
            {
                Resource resource = GetComponentInChildren<Resource>();

                if (resource != null)
                {
                    ResourceBrought?.Invoke();
                    Destroy(resource.gameObject);
                }

                _target = Vector3.zero;
                IsFree = true;
            }            
        }
    }

    private void TakeResource(GameObject resource)
    {
        resource.transform.SetParent(gameObject.transform);
        resource.transform.localPosition = new Vector3(0, 12, 0);
    }
}
