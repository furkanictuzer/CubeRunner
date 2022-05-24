using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvents : MonoBehaviour
{
    public void NextScene()
    {
        var sceneCount = SceneManager.sceneCountInBuildSettings;
        var currentScene = SceneManager.GetActiveScene().buildIndex + 1;

        SceneManager.LoadScene(sceneCount > currentScene ? currentScene : 0);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
