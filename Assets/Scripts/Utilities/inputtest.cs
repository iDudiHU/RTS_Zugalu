using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputtest : MonoBehaviour
{
    private InputManager inputManager;

	private void Start()
	{
        inputManager = InputManager.instance;
    }
	// Update is called once per frame
	void Update()
    {
        Debug.Log(inputManager.horizontalAxis);
        Debug.Log(inputManager.verticalAxis);
    }
}
