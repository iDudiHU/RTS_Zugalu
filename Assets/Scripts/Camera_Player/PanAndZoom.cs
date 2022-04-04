using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PanAndZoom : MonoBehaviour
{
    [Header("Camera Pan and Zoom settings")]
    [Tooltip("The speed witch player can move around the map.")]
    [SerializeField]
    private float panSpeed = 10;
    [Tooltip("The horizontal movmeent input of the player.")]
    [SerializeField]
    private float rotationSpeed = 160;
    [Tooltip("The horizontal movmeent input of the player.")]
    [SerializeField]
    private float movementTime = 5;
    [Tooltip("The zoom speed of the camera.")]
    [SerializeField]
    private float zoomSpeed = 1;
    private float zoomDistance;
    [SerializeField]
    private float zoomMinDistance = 4;
    [SerializeField]
    private float zoomMaxDistance = 25;


    private Vector3 newPosition;
    private Vector3 newZoomPosition;

    InputManager inputManager;

    private CinemachineInputProvider inputProvider;
    private CinemachineVirtualCamera virtualCamera;
    private Transform cameraTransform;
    private Transform cameraHelperTransform;
    private Transform followTransform;

    
    
    void Awake()
	{
        //Grab Cinemachine references
        inputProvider = GetComponent<CinemachineInputProvider>();
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        //Set up inputManager
        inputManager = InputManager.instance;
        //Camrea Transform
        cameraTransform = virtualCamera.VirtualCameraGameObject.transform;
        //Camera Helper Transform
        cameraHelperTransform = cameraTransform.parent.transform;
	}
    void Start()
    {
        //Set starters
        newPosition = cameraHelperTransform.position;
        newZoomPosition = cameraHelperTransform.position;
    }

    void Update()
    {
        
        float x = inputManager.horizontalAxis;
        float y = inputManager.verticalAxis;
        float z = inputProvider.GetAxisValue(2);
		if (followTransform != null)
		{
            cameraHelperTransform.position = followTransform.position;
		}
		else
		{
            if (InputIsBeingProvided())
			{
                RotateScreen(inputManager.rotateAxis);
                PanScreen(inputManager.horizontalAxis, inputManager.verticalAxis);
                ZoomScreen(inputManager.zoomAxis);
			}
            
		}
    }

    private void HandleCamMovement ( float horizontalAxis, float vertcalAxis)
	{
        PanScreen(horizontalAxis, vertcalAxis);
	}
    //Get a direction to move to (local space)
    public void PanDirection(float x, float y)
	{
        newPosition = Vector3.zero;
		if (y == 1f)
		{
            newPosition += (cameraHelperTransform.forward * panSpeed);
		}
        if (y == -1f)
        {
            newPosition += (cameraHelperTransform.forward * -panSpeed);
        }
        if (x == 1f)
        {
            newPosition += (cameraHelperTransform.right * panSpeed);
        }
        if (x == -1f)
        {
            newPosition += (cameraHelperTransform.right * -panSpeed);
        }
    }

    //Moves Camera with WASD and Arrows
    public void PanScreen(float x, float y) 
    {
        PanDirection(x, y);
        cameraHelperTransform.position = Vector3.Lerp(cameraHelperTransform.position, (cameraHelperTransform.position + newPosition), Time.deltaTime * movementTime);
    }

    public void ZoomScreen(float zoomAmmount)
	{
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, (cameraTransform.position + cameraTransform.forward * zoomAmmount * zoomSpeed), Time.deltaTime);
        Vector3 offset = cameraTransform.position - cameraHelperTransform.position;
        cameraTransform.position = cameraHelperTransform.position + ClampMagnitudeWithMin(offset, zoomMaxDistance, zoomMinDistance);
    }
    //Rotates Camera with Q and E
    public void RotateScreen(float rotateAmmount)
	{
        Quaternion newRotation = cameraHelperTransform.rotation;
        newRotation *= Quaternion.Euler(Vector3.up * rotateAmmount);
        cameraHelperTransform.rotation = Quaternion.Lerp(cameraHelperTransform.rotation, newRotation, Time.deltaTime * rotationSpeed);

    }

    public Vector3 ClampMagnitudeWithMin(Vector3 v, float max, float min)
    {
        double sm = v.sqrMagnitude;
        if (sm > (double)max * (double)max) return v.normalized * max;
        else if (sm < (double)min * (double)min) return v.normalized * min;
        return v;
    }

    private bool InputIsBeingProvided()
	{
        return (inputManager.verticalAxis != 0 || inputManager.horizontalAxis != 0 || inputManager.rotateAxis != 0 || inputManager.zoomAxis != 0);
	}
}
