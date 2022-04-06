using UnityEngine;
using UnityEngine.SceneManagement;

//Class that loads the next level
public class LoadLevelButton : MonoBehaviour
{
    [Tooltip("Scriptable Object containing player name")]
    [SerializeField]
    private NameSO _playerName;
    [Tooltip("Gameobject under Input field to show that field needs an input")]
    [SerializeField]
    private GameObject _errorCheck;
    public void LoadLevelByNumber(int levelToLoadNumber)
    {
		if (!string.IsNullOrEmpty(_playerName.PlayerInput))
		{
            SceneManager.LoadScene(levelToLoadNumber);
		}
		else
		{
            _errorCheck.gameObject.SetActive(true);
		}
        
    }
}
