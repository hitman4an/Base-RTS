using UnityEngine;

[RequireComponent(typeof(CoordinatesGenerator))]
public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Unit _unit;

    private CoordinatesGenerator _generator;

    private float _minXOffset = -6f;
    private float _maxXOffset = 6f;
    private float _minZOffset = -7f;
    private float _maxZOffset = 7f;
    private float _radius = 3f;

    private void Awake()
    {
        _generator = GetComponent<CoordinatesGenerator>();
    }
    
    public Unit SpawnUnit(Base unitBase)
    {
        float minX = transform.position.x + _minXOffset;
        float maxX = transform.position.x + _maxXOffset;
        float minZ = transform.position.z + _minZOffset;
        float maxZ = transform.position.z + _maxZOffset;
        
        Vector3 spawnPosition = _generator.GetCoordinates(minX, maxX, minZ, maxZ, _radius);

        if (spawnPosition != Vector3.zero)
        {
            Unit unit = Instantiate(_unit, spawnPosition, Quaternion.identity);

            unit.SetBase(unitBase);
            return unit;
        }

        return null;
    }
}
