using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScanResourcesStorage))]
public class BaseScanner : MonoBehaviour
{
    [SerializeField] private float _scanRadius = 15;
    [SerializeField] private LayerMask _resourceMask;    

    private ScanResourcesStorage _scanStorage;    

    private void Awake()
    {
        _scanStorage = GetComponent<ScanResourcesStorage>();
    }

    public void Scan()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _scanRadius, _resourceMask);

        foreach (var collider in colliders)
        {
            Resource resource = collider.GetComponent<Resource>();

            if (resource != null)
            {
                _scanStorage.AddResource(resource);
            }
        }
    }
}
