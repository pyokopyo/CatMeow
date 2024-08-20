using UnityEngine;
using UnityEditor;

public class Controller : MonoBehaviour
{


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) { DisplayUEPopup(); }
    }

    private void DisplayUEPopup()
    {
            EditorUtility.DisplayDialog("あああ",
                "いいい", "ううう", "えええ");
    }

}
