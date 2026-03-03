using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitSpawner))]
public class Base : MonoBehaviour
{
    [SerializeField] private int unitCount = 3;
    [SerializeField] private float _scanRadius = 5;
    [SerializeField] private LayerMask _resourceMask;
    [SerializeField] private float _scanDelay = 2f;

    private UnitSpawner _spawner;
    private List<Unit> _units = new List<Unit>();
    private Coroutine _coroutine;
    
    private int _resourceCount = 0;
    private bool _isActive = true;

    private void Awake()
    {
        _spawner = GetComponent<UnitSpawner>();
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        
        _isActive = false;
    }

    private void Start()
    {
        for (int i = 0; i < unitCount; i++)
        {
            Unit unit = _spawner.SpawnUnit(this);
            unit.ResourceBrought += IncreaseResources;

            if (unit != null)
            { 
                _units.Add(unit);
            }
        }

        _coroutine = StartCoroutine(ScanResources());
    }

    private IEnumerator ScanResources()
    {
        var wait = new WaitForSeconds(_scanDelay);

        while (_isActive)
        {
            if (!HasFreeUnits())
            {
                yield return wait;
            }
                        
            Collider[] colliders = Physics.OverlapSphere(transform.position, _scanRadius, _resourceMask);

            foreach (var collider in colliders)
            {
                Resource resource = collider.GetComponent<Resource>();

                if (resource.CanCollect)
                {
                    Unit unit = GetFreeUnit();

                    if (unit)
                    {
                        unit.GetResource(resource.gameObject.transform.position);
                        resource.setCanCollect(false);
                    }
                }

                if (!HasFreeUnits())
                {
                    break;
                }
            }

            yield return wait;
        }
    }

    private Unit GetFreeUnit()
    {
        foreach(var unit in _units)
        {
            if (unit.IsFree)
            {
                return unit;
            }
        }

        return null;
    }

    private bool HasFreeUnits()
    {
        foreach (var unit in _units)
        {
            if (unit.IsFree)
            {
                return true;
            }
        }

        return false;
    }

    private void IncreaseResources()
    {
        _resourceCount++;
    }
}
