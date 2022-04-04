using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    // A global instance for scripts to reference
    public static InputManager instance;

    private void Awake()
    {
        // Set up the instance of this
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Header("Player Axis Input")]
    [Tooltip("The horizontal input of the player.")]
    public float horizontalAxis;
    [Tooltip("The vertical input of the player.")]
    public float verticalAxis;
    [Tooltip("The rotational input of the player.")]
    public float rotateAxis;
    [Tooltip("The zoom input of the player.")]
    public float zoomAxis;

    public void ReadRotateInput(InputAction.CallbackContext context)
    {
        float inputVector = context.ReadValue<float>();
        rotateAxis = inputVector;
    }

    public void ReadKeyboardPanInput(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        horizontalAxis = inputVector.x;
        verticalAxis = inputVector.y;
    }

    public void ReadZoomInput(InputAction.CallbackContext context)
    {
        float mouseScrollInput = context.ReadValue<float>();
        zoomAxis = mouseScrollInput;

    }
}