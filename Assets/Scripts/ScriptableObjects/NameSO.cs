using UnityEngine;

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
