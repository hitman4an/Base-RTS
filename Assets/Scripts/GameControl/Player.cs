using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(RayCaster))]
public class Player : MonoBehaviour
{
    [SerializeField] private ResourcesUI _resources;
    [SerializeField] private int _firstBaseUnitCount = 3;
    [SerializeField] private BaseSpawner _baseSpawner;
    
    private Base _selectedBase;
    private RayCaster _raycaster;    

    private bool _isFirstBase = true;

    private void Awake()
    {
        _raycaster = GetComponent<RayCaster>();        
    }

    private void OnEnable()
    {
        _raycaster.BaseClick += OnBaseClick;
        _raycaster.ActionClick += OnActionClick;
        _raycaster.EmptyClick += OnEmptyClick;
    }

    private void OnDisable()
    {
        _raycaster.BaseClick -= OnBaseClick;
        _raycaster.ActionClick -= OnActionClick;
        _raycaster.EmptyClick -= OnEmptyClick;
    }

    private void OnBaseClick(Base unitBase)
    {
        if (_selectedBase)
        {
            _selectedBase.UnsetActiveSelection();
            _resources.DeactivateBaseGUI();
        }
        
        _selectedBase = unitBase;
        _selectedBase.SetActiveSelection();
        _resources.ActivateBaseGUI(unitBase);
    }

    private void OnActionClick(Vector3 target)
    {
        if (_selectedBase)
        {
            _selectedBase.SetFlag(target);
        }
    }

    private void OnEmptyClick(Vector3 target)
    {
        if (_isFirstBase)
        {
            _isFirstBase = false;
            _baseSpawner.Spawn(target, _firstBaseUnitCount);
        }
        
        if (_selectedBase)
        {
            _selectedBase.UnsetActiveSelection();
            _resources.DeactivateBaseGUI();
            _selectedBase = null;
        }
    }
}
