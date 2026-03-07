using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitSpawner))]
[RequireComponent(typeof(BaseScanner))]
[RequireComponent(typeof(ScanResourcesStorage))]
public class UnitHandler : MonoBehaviour
{
    public event Action BaseBuilt;

    [SerializeField] private float _scanDelay = 2f;

    private UnitSpawner _spawner;
    private BaseScanner _scanner;
    private ScanResourcesStorage _storage;
    private Coroutine _collectCoroutine;
    private Coroutine _buildCoroutine;

    private bool _isActive = true;
    private bool _isWaitingFreeUnitForConstruction = false;
    private float _buildDelay = 2f;
    private List<Unit> _units = new List<Unit>();

    private void Awake()
    {
        _spawner = GetComponent<UnitSpawner>();
        _scanner = GetComponent<BaseScanner>();
        _storage = GetComponent<ScanResourcesStorage>();
    }
    
    private void OnDisable()
    {
        if (_collectCoroutine != null)
            StopCoroutine(_collectCoroutine);

        if (_buildCoroutine != null)
            StopCoroutine(_buildCoroutine);

        _isActive = false;
    }

    private void Start()
    {
        _collectCoroutine = StartCoroutine(CollectResources());
    }

    public void SpawnUnits(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnUnit();
        }
    }

    public void SpawnUnit()
    {
        Unit unit = _spawner.SpawnUnit();

        if (unit != null)
        {
            _units.Add(unit);
        }
    }

    public void AddToBase(Unit unit)
    {
        _units.Add(unit);        
        unit.MoveToPosition(GetNearBasePosition());
    }

    public void RemoveUnit(Unit unit)
    {
        _units.Remove(unit); 
    }

    public void CreateBase(Vector3 target)
    {
        _isWaitingFreeUnitForConstruction = true;
        _buildCoroutine = StartCoroutine(BuildBase(target));
    }

    public Vector3 GetNearBasePosition()
    {
        return _spawner.GetNearBasePosition();
    }

    public int GetCount()
    {
        return _units.Count;
    }

    private Unit GetFreeUnit()
    {
        foreach (var unit in _units)
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

    private IEnumerator CollectResources()
    {
        var wait = new WaitForSeconds(_scanDelay);

        while (_isActive)
        {
            _scanner.Scan();

            if (HasFreeUnits() != false)
            {
                List<Resource> resources = _storage.GetFreeResources();

                if (resources.Count > 0)
                {
                    foreach(Resource resource in resources)
                    {
                        Unit unit = GetFreeUnit();

                        if (unit != null)
                        {
                            _storage.RemoveFindedResource(resource);
                            unit.MoveToResource(resource.gameObject.transform.position);
                        }
                    }
                }
            }

            yield return wait;
        }
    }

    private IEnumerator BuildBase(Vector3 target)
    {
        var wait = new WaitForSeconds(_buildDelay);

        while (_isWaitingFreeUnitForConstruction)
        {
            Unit unit = GetFreeUnit();

            if (unit != null)
            {
                unit.MoveToBuild(target);
                unit.BaseBuilt += OnBaseBuilt;
                _isWaitingFreeUnitForConstruction = false;
            }

            yield return wait;
        }
    }

    private void OnBaseBuilt(Unit unit)
    {
        unit.BaseBuilt -= OnBaseBuilt;

        BaseBuilt?.Invoke();
    }
}
