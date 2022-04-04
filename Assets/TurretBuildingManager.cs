using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GroundPlacementController : MonoBehaviour
{
    [SerializeField]
    private Camera Camera;
    [SerializeField]
    private GameObject _placeableObject;
    [SerializeField]
    private LayerMask _buildableLayer;

    private float mouseWheelRotation;
    private int currentPrefabIndex = -1;

    private RTSControl _inputActions;

    private void Update()
    {
        if (_placeableObject != null)
        {
            MoveCurrentObjectToMouse();
            RotateFromMouseWheel();
            ReleaseIfClicked();
        }
    }

    private bool PressedKeyOfCurrentPrefab()
    {
        return _placeableObject != null;
    }

    private void HandleTurretObject()
    {
        if (PressedKeyOfCurrentPrefab() && _placeableObject != null)
        {
            Destroy(_placeableObject);
            currentPrefabIndex = -1;
        }
        else
        {
            if (_placeableObject != null)
            {
                Destroy(_placeableObject);
            }

            _placeableObject = Instantiate(_placeableObject[i]);
            currentPrefabIndex = i;
        }
    }
    private void MoveCurrentObjectToMouse()
    {
        if (Physics.Raycast(Camera.ScreenPointToRay(_inputActions.GameplayActionMap.MousePosition.ReadValue<Vector2>()), out RaycastHit hitInfo, 5000000f, _buildableLayer)
        {
            _placeableObject.transform.position = hitInfo.point;
            _placeableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
    }

    private void RotateFromMouseWheel()
    {
        mouseWheelRotation = _inputActions.GameplayActionMap.ZoomCamera.ReadValue<Vector2>().y;
        _placeableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    }

    private void ReleaseIfClicked()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            _placeableObject = null;
        }
    }
}