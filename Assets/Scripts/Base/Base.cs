using UnityEngine;

[RequireComponent(typeof(ResourceCounter))]
[RequireComponent(typeof(ScanResourcesStorage))]
[RequireComponent(typeof(UnitHandler))]
[RequireComponent(typeof(FlagSetter))]
public class Base : MonoBehaviour
{
    private const string Edge = "Edge";
    private enum BaseState
    {
        CreatingUnits,
        CreatingBases
    }

    [SerializeField] private int _unitPrice = 3;
    [SerializeField] private int _basePrice = 5;

    private ResourceCounter _counter;    
    private UnitHandler _handler;
    private FlagSetter _flagSetter;

    private bool _isSelected;
    private BaseState _state = BaseState.CreatingUnits;

    private void Awake()
    {
        _counter = GetComponent<ResourceCounter>();        
        _handler = GetComponent<UnitHandler>();
        _flagSetter = GetComponent<FlagSetter>();            
    }

    private void OnEnable()
    {
        _handler.BaseBuilt += OnBaseBuilt;
    }

    public void Initialize(int unitCount)
    {
        if (unitCount > 0)
        {
            _handler.SpawnUnits(unitCount);
        }
    }

    public void Initialize(Unit unit)
    {
        _state = BaseState.CreatingUnits;
        _handler.AddToBase(unit);
        unit.SetBase(this);
    }

    public void ResourceTaken(Resource resource, Unit unit)
    {
        _counter.IncreaseResources();
        ResourceStorage.Instance.RemoveBusyResource(resource);

        unit.MoveToPosition(_handler.GetNearBasePosition());
        
        if (_state == BaseState.CreatingUnits && _counter.Count == _unitPrice)
        {
            _handler.SpawnUnit();
            _counter.DecreaseResources(_unitPrice);
        }

        if (_state == BaseState.CreatingBases && _counter.Count == _basePrice)
        {
            _state = BaseState.CreatingUnits;
            _handler.CreateBase(_flagSetter.GetPosition());
            _counter.DecreaseResources(_basePrice);
            _flagSetter.ChangePositionState(false);
        }
    }

    public void SetActiveSelection()
    {
        if (_isSelected == false)
        {
            Transform selection = transform.Find(Edge);

            if (selection != null) 
            {
                _flagSetter.ChangeVisibleState(true);
                selection.gameObject.SetActive(true);                
                _isSelected = true;
            }
        }
    }

    public void UnsetActiveSelection()
    {
        if (_isSelected)
        {
            Transform selection = transform.Find(Edge);

            if (selection != null)
            {
                _flagSetter.ChangeVisibleState(false);
                selection.gameObject.SetActive(false);                
                _isSelected = false;
            }
        }
    }

    public void SetFlag(Vector3 target)
    {
        if (_isSelected && _handler.GetCount() > 1)
        {
            bool isFlagSet = _flagSetter.Set(target);

            if (isFlagSet)
            {
                _state = BaseState.CreatingBases;
            }
        }
    }

    public void RemoveUnit(Unit unit)
    {
        _handler.RemoveUnit(unit);
    }

    public ResourceCounter GetCounter()
    {
        return _counter;
    }

    private void OnBaseBuilt()
    {
        _flagSetter.Unset();
        _flagSetter.ChangePositionState(true);
    }
}
