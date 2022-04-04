using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class which destroys it's gameobject after a certain amount of time
public class TimedObjectDestroyer : MonoBehaviour
{
    [Tooltip("The lifetime of this gameobject")]
    public float lifetime = 5.0f;

    // The amount of time this gameobject has already existed in play mode
    private float _timeAlive = 0.0f;

    [Tooltip("Whether to destroy child gameobjects when this gameobject is destroyed")]
    public bool destroyChildrenOnDeath = true;

    // Flag which tells whether the application is shutting down (helps avoid errors)
    public static bool quitting = false;

    private void OnApplicationQuit()
    {
        quitting = true;
        DestroyImmediate(this.gameObject);
    }

    /// Every frame, increment the amount of time that this gameobject has been alive, or if it has exceeded it's maximum lifetime, destroy it
    void Update()
    {
        if (_timeAlive > lifetime)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _timeAlive += Time.deltaTime;
        }
    }

    /// Behavior which triggers when this component is destroyed
    private void OnDestroy()
    {
        if (destroyChildrenOnDeath && !quitting && Application.isPlaying)
        {
            int childCount = transform.childCount;
            for (int i = childCount - 1; i >= 0; i--)
            {
                GameObject childObject = transform.GetChild(i).gameObject;
                if (childObject != null)
                {
                    Destroy(childObject);
                }
            }
        }
        transform.DetachChildren();
    }
}
