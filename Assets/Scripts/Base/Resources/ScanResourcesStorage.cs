using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanResourcesStorage : MonoBehaviour
{
    private List<Resource> _findedResources = new List<Resource>();
    
    public void AddResource(Resource resource)
    {
        if (ResourceStorage.Instance.CheckBusyResources(resource) == false)
        {
            _findedResources.Add(resource);
        }
    }

    public List<Resource> GetFreeResources()
    {
        List<Resource> result = new List<Resource>();
        
        foreach (Resource resource in _findedResources)
        {
            if (ResourceStorage.Instance.CheckFreeResource(resource))
            {
                result.Add(resource);
            }
        }

        return result;
    }

    public void RemoveFindedResource(Resource resource)
    {
        ResourceStorage.Instance.ClearFreeResource(resource);
        _findedResources.Remove(resource);
    }
}
