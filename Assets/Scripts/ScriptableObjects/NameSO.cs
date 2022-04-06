using UnityEngine;
//Scriptbale object to save the text input between scenes
[CreateAssetMenu]
public class NameSO : ScriptableObject
{
	[SerializeField]
    private string _playerInput;
	public string PlayerInput
	{
		get { return _playerInput; }
		set { _playerInput = value; }
	}

}
