using UnityEngine;
using System;

[RequireComponent(typeof(UnitResourceTaker))]
[RequireComponent(typeof(UnitMover))]
[RequireComponent(typeof(BaseBuilder))]
public class Unit : MonoBehaviour
{
    public event Action<Unit> BaseBuilt;
    
    private UnitResourceTaker _taker;
    private UnitMover _mover;
    private BaseBuilder _builder;    

    private UnitState _state = UnitState.ColectResources;

    public bool IsFree { get; private set; } = true;

    private enum UnitState
    {
        ColectResources,
        BuildingBases
    }

    private void Awake()
    {
        _taker = GetComponent<UnitResourceTaker>();
        _mover = GetComponent<UnitMover>();
        _builder = GetComponent<BaseBuilder>();
    }

    private void OnEnable()
    {
        _taker.ResourceBrought += OnResourceTaken;
        _mover.TargetReached += OnTargetReached;
    }

    private void OnDisable()
    {
        _taker.ResourceBrought -= OnResourceTaken;
        _mover.TargetReached -= OnTargetReached;
    }

    public void MoveToResource(Vector3 target)
    {
        IsFree = false;
        _taker.SetTarget(target);        
    }

    public void MoveToBuild(Vector3 target)
    {
        IsFree = false;
        _state = UnitState.BuildingBases;
        _mover.MoveToTarget(target);
    }

    public void MoveToPosition(Vector3 target)
    {
        IsFree = false;
        _mover.MoveToTarget(target);
    }

    public void SetBase(Base unitBase)
    {
        _taker.SetBase(unitBase);
    }

    private void OnResourceTaken()
    {
        IsFree = true;
    }

    private void OnTargetReached()
    {
        if (_state == UnitState.BuildingBases) 
        {
            _builder.Build(this);
            _state = UnitState.ColectResources;
            BaseBuilt?.Invoke(this);
        }
        
        IsFree = true;
    }
}
