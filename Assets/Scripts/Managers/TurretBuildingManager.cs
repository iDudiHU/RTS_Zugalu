using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretBuildingManager : MonoBehaviour
{
    [SerializeField]
    private Camera Camera;
    private GameObject _placeableObject;
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private Transform _parent;
    [SerializeField]
    private LayerMask _buildableLayer;

    private bool _clicked;

    private float mouseWheelRotation;

    private RTSControl _inputActions;

    private void Awake()
    {
        _inputActions = new RTSControl();
    }
    private void Update()
    {

        if (_placeableObject != null && _clicked)
        {
            MoveCurrentObjectToMouse();
            RotateFromMouseWheel();
            ReleaseIfClicked();
        }
    }

    public void OnButtonPess()
    {
        if (_placeableObject != null)
        {
            Destroy(_placeableObject);
            _clicked = false;

        }
            _placeableObject = Instantiate(_prefab, _parent);
        _clicked = true;
    }
    private void MoveCurrentObjectToMouse()
    {
        if (Physics.Raycast(Camera.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hitInfo, 5000000f, _buildableLayer))
        {
            _placeableObject.transform.position = hitInfo.point;
            _placeableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
    }

    private void RotateFromMouseWheel()
    {
        mouseWheelRotation += Mouse.current.scroll.ReadValue().y/120;
        _placeableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    }

    private void ReleaseIfClicked()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            _placeableObject = null;
        }
    }
}