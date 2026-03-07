using UnityEngine;

[RequireComponent(typeof(CoordinatesGenerator))]
[RequireComponent(typeof(Base))]
public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Unit _unit;

    private CoordinatesGenerator _generator;
    private Base _unitBase;

    private float _minXOffset = -6f;
    private float _maxXOffset = 6f;
    private float _minZOffset = -7f;
    private float _maxZOffset = 7f;
    private float _radius = 3f;

    private void Awake()
    {
        _generator = GetComponent<CoordinatesGenerator>();
        _unitBase = GetComponent<Base>();
    }
    
    public Unit SpawnUnit()
    {
        Vector3 spawnPosition = GetNearBasePosition();

        if (spawnPosition != Vector3.zero)
        {
            Unit unit = Instantiate(_unit, _unitBase.transform.position, Quaternion.identity);

            unit.SetBase(_unitBase);
            unit.MoveToPosition(spawnPosition);

            return unit;
        }

        return null;
    }

    public Vector3 GetNearBasePosition()
    {
        float minX = transform.position.x + _minXOffset;
        float maxX = transform.position.x + _maxXOffset;
        float minZ = transform.position.z + _minZOffset;
        float maxZ = transform.position.z + _maxZOffset;

        Vector3 target = _generator.GetCoordinates(minX, maxX, minZ, maxZ, _radius);

        if (target != Vector3.zero) 
        { 
            return target;
        }
        else
        {
            return new Vector3(minX, 0, minZ);
        }        
    }
}
