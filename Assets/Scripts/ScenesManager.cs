using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class ScenesManager : MonoBehaviour
{
    private static bool created = false;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);
        }
    }

    public async void AwakeLoadScene() 
    {
        var result = await LoadScene();  
        Debug.Log(result);  // true
    }

    public async UniTask<bool> LoadScene()
    {
        EnableLoadPanel();

        if (SceneManager.GetActiveScene().name == "Main")
        {
            SceneManager.LoadScene("Sub0", LoadSceneMode.Single);
        }
        else if (SceneManager.GetActiveScene().name == "Sub0")
        {
            SceneManager.LoadScene("Sub1", LoadSceneMode.Single);
        }
        else if (SceneManager.GetActiveScene().name == "Sub1")
        {
            SceneManager.LoadScene("Sub0", LoadSceneMode.Single);
        }
        await UniTask.Delay(1000);
        DisbleLoadPanel();
        return true;
    }

    [SerializeField] private GameObject _loadPanel;
    public void DisbleLoadPanel()
    {
        _loadPanel.SetActive(false);
    }
    public void EnableLoadPanel()
    {
        _loadPanel.SetActive(true);
    }
}
