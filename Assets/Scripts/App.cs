using UnityEngine;

public class App : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Quit();
        }
    }
    
    public void Quit() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}