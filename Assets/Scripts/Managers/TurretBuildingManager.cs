using System;
using UnityEngine;
using UnityEngine.InputSystem;
//Class that allows to build turrets
public class TurretBuildingManager : MonoBehaviour
{
    [Tooltip("Main Camera")]
    [SerializeField]
    private Camera Camera;
    private GameObject _placeableObject;
    [Tooltip("Placeble prefab")]
    [SerializeField]
    private GameObject _prefab;
    [Tooltip("Holder to keep the scene clean")]
    [SerializeField]
    private Transform _parent;
    [Tooltip("Layer to controll where to and where not to build")]
    [SerializeField]
    private LayerMask _buildableLayer;
    [SerializeField]
    private LayerMask _turretLayer;

    private float mouseWheelRotation;

    private RTSControl _inputActions;


    private void Awake()
    {
        //assign input actions
        _inputActions = new RTSControl();
    }
    private void Update()
    {

        if (_placeableObject != null)
        {
            MoveCurrentObjectToMouse();
            RotateFromMouseWheel();
            CancelBuild();
            ConfirmBuild();
        }
    }

	public void OnButtonPess()
    {
        //Destroy if already exist
        if (_placeableObject != null)
        {
            Destroy(_placeableObject);

        }
            _placeableObject = Instantiate(_prefab, _parent);
    }
    private void MoveCurrentObjectToMouse()
    {
        //Raycast on the buildable layer
        if (Physics.Raycast(Camera.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hitInfo, 5000000f, _buildableLayer))
        {
            _placeableObject.transform.position = hitInfo.point;
            _placeableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
		else
		{
            //if the layer is not buildable make the turret not appear
            _placeableObject.transform.position = Vector3.zero;
            _placeableObject.transform.rotation = Quaternion.Euler(0,0,0);
        }
    }

    private void RotateFromMouseWheel()
    {
        //rotate turret 
        mouseWheelRotation += Mouse.current.scroll.ReadValue().y/120;
        _placeableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    }

    private void ConfirmBuild()
    {
        //if it is on buildable layer
        if (Physics.Raycast(Camera.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hitInfo, 5000000f, _buildableLayer))
		{
            //find colliders in range to make sure not to build on eachother
            Collider[] hitColliders = Physics.OverlapSphere(hitInfo.point, 2f, _turretLayer);
            if (Mouse.current.leftButton.wasPressedThisFrame && !_placeableObject.transform.position.Equals(Vector3.zero) && hitColliders.Length == 1)
            {
                _placeableObject = null;
            }
            
        }
            
        
    }

    private void CancelBuild()
	{
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Destroy(_placeableObject);
        }
    }
}