using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class RayCaster : MonoBehaviour
{
    public event Action<Base> BaseClick;
    public event Action<Vector3> ActionClick;
    public event Action<Vector3> EmptyClick;

    private PlayerInput _input;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        _input.SelectButtonPressed += GetSelection;
        _input.ActionButtonPressed += GetActionPosition;
    }

    private void GetSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.TryGetComponent(out Base unitBase))
            {
                BaseClick?.Invoke(unitBase);
            }
            else
            {
                EmptyClick?.Invoke(hit.point);
            }
        }
    }

    private void GetActionPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.TryGetComponent(out Terrain terrain))
            {
                ActionClick?.Invoke(hit.point);
            }
        }
    }
}
