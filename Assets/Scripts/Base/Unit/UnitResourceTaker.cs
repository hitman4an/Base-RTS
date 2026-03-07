using UnityEngine;
using System;

[RequireComponent(typeof(UnitMover))]
public class UnitResourceTaker : MonoBehaviour
{
    public event Action ResourceBrought;

    private Vector3 _target;
    private Base _base;
    private UnitMover _mover;
    private Unit _unit;

    private void Awake()
    {
        _mover = GetComponent<UnitMover>();
        _unit = GetComponent<Unit>();
    }

    public void SetTarget(Vector3 target)
    {
        _target = target;
        _mover.MoveToTarget(_target);
    }

    public void SetBase(Base unitBase) 
    { 
        _base = unitBase; 
    }

    public void ChangeBase(Base unitBase)
    {
        _base.RemoveUnit(_unit);
        _base = unitBase;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.position == _target)
        {
            if (other.gameObject.GetComponent<Resource>())
            {
                TakeResource(other.gameObject);
                SetTarget(_base.transform.position);
            }
            else
            {
                Resource resource = GetComponentInChildren<Resource>();

                if (resource != null)
                {
                    _base.ResourceTaken(resource, _unit);
                    ResourceBrought?.Invoke();
                    Destroy(resource.gameObject);                    
                }                
            }
        }
    }

    private void TakeResource(GameObject resource)
    {
        resource.transform.SetParent(gameObject.transform);
        resource.transform.localPosition = new Vector3(0, 12, 0);
    }
}
