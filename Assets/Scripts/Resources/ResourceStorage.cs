using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceStorage: MonoBehaviour
{
    public static ResourceStorage Instance { get; private set; }
    
    private List<Resource> _freeResources = new List<Resource>();
    private List<Resource> _busyResources = new List<Resource>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public bool CheckFreeResource(Resource resource)
    {
        return _freeResources.Contains(resource);
    }

    public bool CheckBusyResources(Resource resource)
    {
        return _busyResources.Contains(resource);
    }

    public void ClearFreeResource(Resource resource)
    {
        _freeResources.Remove(resource);
        _busyResources.Add(resource);
    }

    public void RemoveBusyResource(Resource resource)
    {
        _busyResources.Remove(resource);
    }

    public void AddFreeResource(Resource resource)
    {
        if (_busyResources.Contains(resource) == false && _freeResources.Contains(resource) == false)
        {
            _freeResources.Add(resource);
        }
    }
}
