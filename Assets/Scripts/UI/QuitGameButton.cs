using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// Class that closes the game or exits play mode depending on the case
public class QuitGameButton : MonoBehaviour
{
    
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
