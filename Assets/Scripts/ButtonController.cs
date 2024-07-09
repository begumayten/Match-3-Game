using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void LoadLevelScene(string sceneName)
    {
        Debug.Log("button clicked");
        SceneManager.LoadScene(sceneName);
    }
}
