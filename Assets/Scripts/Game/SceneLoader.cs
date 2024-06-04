using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance {  get; private set; }

    [SerializeField] private GameLoadingScreen loadingScreen;
    [SerializeField] private Slider loadSlider;

    public AsyncOperation loadingOperation { get; private set; }
    public float progress { get; private set; } = 0f;
    public bool canActivate { get; private set; } = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator LoadScene(LevelDataSO data)
    {
        yield return null;
    }

    public IEnumerator LoadScene_(string sceneName)
    {
        loadingScreen.SetPanelActive(true);

        loadingOperation = SceneManager.LoadSceneAsync(sceneName);
        loadingOperation.allowSceneActivation = false;

        while (!loadingOperation.isDone)
        {
            if (loadingOperation.progress >= 0.9f)
            {
                loadingScreen.SetSliderValue(1f);
                loadingOperation.allowSceneActivation = true;
            }
            else
            {
                loadingScreen.SetSliderValue(loadingOperation.progress);
            }

            yield return null;
        }

        loadingScreen.SetPanelActive(false);
    }
}
