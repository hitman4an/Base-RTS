using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuilder : MonoBehaviour
{
    [SerializeField] private Base _prefab;
    
    public void Build(Unit unit)
    {
        Base unitBase = Instantiate(_prefab, transform.position, Quaternion.identity);

        unitBase.Initialize(unit);
    }
}
