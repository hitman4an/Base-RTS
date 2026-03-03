using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CoordinatesGenerator))]
public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnSpeed = 5f;
    [SerializeField] private int _spawnCount = 50;
    [SerializeField] private Resource _resource;

    private Coroutine _coroutine;
    private CoordinatesGenerator _generator;

    private float _minX = 1;
    private float _maxX = 999;
    private float _minZ = 1;
    private float _maxZ = 999;

    private float _radius = 5f;
    private bool _isActive = true;

    private void Awake()
    {
        _generator = GetComponent<CoordinatesGenerator>();
    }

    private void Start()
    {
        _coroutine = StartCoroutine(GetResources());
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        
        _isActive = false;
    }

    private IEnumerator GetResources()
    {
        var wait = new WaitForSeconds(_spawnSpeed);

        yield return wait;

        while (_isActive)
        {
            for (int i = 0; i < _spawnCount; i++)
            {
                SpawnResource();
            }

            yield return wait;
        }
    }

    private void SpawnResource()
    {
        Vector3 spawnPosition = _generator.GetCoordinates(_minX, _maxX, _minZ, _maxZ, _radius);

        if (spawnPosition != Vector3.zero)
        {
            Instantiate(_resource, spawnPosition, Quaternion.identity);
        }
    }
}
