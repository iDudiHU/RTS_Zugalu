using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//Class that handles saving the name
public class UI_StringInput : MonoBehaviour
{
    private TMP_InputField _inputField;
	[SerializeField]
	private NameSO _nameSO;

	private void Awake()
	{
		_inputField = this.transform.GetComponent<TMP_InputField>();
		_nameSO.PlayerInput = "";
	}

	public void SaveName()
	{
		_nameSO.PlayerInput = _inputField.text;
	}

	//Wanted to handle validation for the input field
	//private char ValidateCharacter(string validCharacters, char addedCharacter)
	//{
	//	if (validCharacters.IndexOf(addedCharacter) != -1)
	//	{
	//		return addedCharacter;
	//	}
	//	else
	//	{
	//		return '\0';
	//	}
	//}
}
