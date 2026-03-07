using System;
using UnityEngine;

public class ResourceCounter : MonoBehaviour
{
    public event Action<int> CountChanged;

    public int Count { get; private set; }

    public void IncreaseResources()
    {
        Count++;
        CountChanged?.Invoke(Count);
    }

    public void DecreaseResources(int amount) 
    {
        Count -= amount;
        CountChanged?.Invoke(Count);
    }
}
