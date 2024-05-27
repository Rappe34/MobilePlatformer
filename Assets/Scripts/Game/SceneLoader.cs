using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance {  get; private set; }

    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Slider loadSlider;

    public AsyncOperation loadingOperation { get; private set; }
    public float progress { get; private set; } = 0f;
    public bool canActivate { get; private set; } = false;

    public IEnumerator LoadScene(string sceneName)
    {
        loadingOperation = SceneManager.LoadSceneAsync(sceneName);
        loadingOperation.allowSceneActivation = false;

        while (!loadingOperation.isDone)
        {
            yield return null;
        }
    }

    public void ActivateScene()
    {
        loadingOperation.allowSceneActivation = true;
    }
}
