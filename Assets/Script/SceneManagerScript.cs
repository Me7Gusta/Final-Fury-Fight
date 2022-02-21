using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;

    private string NextScene = "Menu";

    public void SetNextScene(string scene)
    {
        NextScene = scene;
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene(NextScene);
    }

    public void GoToMenu()
    {
        NextScene = "Menu";
        GoToNextScene();
    }

    public void GoToCutscene()
    {
        NextScene = "Cutscene";
        GoToNextScene();
    }

    public void GoToLevel1()
    {
        NextScene = "Level1";
        //GoToNextScene();
        StartCoroutine(LoadAsynchronously(NextScene));
    }

    public void GoToLevel2()
    {
        NextScene = "Level2";
        GoToNextScene();
    }

    IEnumerator LoadAsynchronously (string level)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(level);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            yield return null;
        }
    }
}
