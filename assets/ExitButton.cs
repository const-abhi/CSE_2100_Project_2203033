using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in Editor
        #else
            Application.Quit(); // Quit the application in build
        #endif
    }
}