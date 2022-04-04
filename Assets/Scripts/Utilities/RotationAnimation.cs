using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that handles animation of a traditional pickup.
/// </summary>
public class RotationAnimation : MonoBehaviour
{
    [Tooltip("The height difference between the resting position of the object and it's maximum or minimum height.")]
    [SerializeField]
    private float oscillationHeight = 0.5f;
    [Tooltip("The speed at which the object oscilates up and down.")]
    [SerializeField]
    private float oscillationSpeed = 2.0f;
    [Tooltip("The speed at which the object rotates per second (in degrees)")]
    [SerializeField]
    private float rotationSpeed = 90.0f;
    // The starting position of the object.
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.localPosition;
    }

    private void LateUpdate()
    {
        transform.localPosition = startPosition + (Vector3.up * oscillationHeight * Mathf.Cos(Time.timeSinceLevelLoad * oscillationSpeed));
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + Vector3.up * Time.deltaTime * rotationSpeed);
    }
}
