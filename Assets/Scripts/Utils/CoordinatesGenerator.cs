using UnityEngine;

public class CoordinatesGenerator: MonoBehaviour
{
    [SerializeField] private LayerMask _objectMask;
    [SerializeField] private LayerMask _terrainMask;
    [SerializeField] private LayerMask _resourceMask;

    public Vector3 GetCoordinates(float minX, float maxX, float minZ, float maxZ, float radius)
    {
        if (_objectMask.value == 0 || _terrainMask.value == 0 || _resourceMask == 0)
        {
            Debug.LogError($"[!] {gameObject.name}: layerMasks are not set!", this);
            return Vector3.zero;
        }
        
        
        bool isValid = false;

        while (isValid == false)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
            bool hasObject = Physics.CheckSphere(spawnPosition, radius, _objectMask);
            bool hasTerrain = Physics.CheckSphere(spawnPosition, radius, _terrainMask);
            bool hasResource = Physics.CheckSphere(spawnPosition, radius, _resourceMask);

            if (!hasObject && !hasResource && hasTerrain)
            {
                isValid = true;
                return spawnPosition;
            }
        }

        return Vector3.zero;
    }
}
