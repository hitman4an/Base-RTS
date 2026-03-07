using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagSetter : MonoBehaviour
{
    [SerializeField] private Flag _flagPrefab;
    [SerializeField] private LayerMask _objectMask;

    private Flag _currentFlag;

    private float _baseRadius = 10f;
    private bool _canChangePosition;

    private void Awake()
    {
        _canChangePosition = true;
    }

    public bool Set(Vector3 target)
    {
        if (_currentFlag)
        {
            if (CanSet(target))
            {
                _currentFlag.transform.position = target;
                return true;
            }
        }
        else
        {
            if (CanSet(target))
            {
                _currentFlag = Instantiate(_flagPrefab, target, Quaternion.identity);
                return true;
            }
        }

        return false;
    }

    public void Unset()
    {
        if (_currentFlag != null)
        {
            Destroy(_currentFlag.gameObject);
            _currentFlag = null;
        }
    }

    public Vector3 GetPosition()
    {
        if (_currentFlag != null)
        {
            return _currentFlag.transform.position;
        }

        return Vector3.zero;
    }

    public void ChangeVisibleState(bool value)
    {
        if (_currentFlag != null)
        {
            _currentFlag.gameObject.SetActive(value);
        }
    }

    public void ChangePositionState(bool value)
    {
        _canChangePosition = value;
    }

    private bool CanSet(Vector3 target)
    {
        if (_canChangePosition == false) 
            return false;
        
        Collider[] colliders = Physics.OverlapSphere(target, _baseRadius, _objectMask);

        foreach (var collider in colliders)
        {
            Base unitBase = collider.GetComponent<Base>();

            if (unitBase != null)
            {
                return false;
            }
        }

        return true;
    }
}
