using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    [SerializeField] Base _basePrefab;
    
    public void Spawn(Vector3 target, int unitCount)
    {
        Base unitBase = Instantiate(_basePrefab, target, Quaternion.identity);

        unitBase.Initialize(unitCount);
    }
}
