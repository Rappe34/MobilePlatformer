using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public SceneLoader Instance { get; private set; }

    private AsyncOperation loadingOperation;

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

    public void LoadScene(LevelDataSO data)
    {
        StartCoroutine(LoadScene_(data));
    }

    public IEnumerator LoadScene_(LevelDataSO data)
    {
        loadingOperation = SceneManager.LoadSceneAsync(data.levelSceneName);
        LoadingScreen.Instance.SetPanelActive(true);

        while (!loadingOperation.isDone)
        {
            if (loadingOperation.progress >= 0.9f)
            {
                LoadingScreen.Instance.SetSliderValue(1f);
            }
            else
            {
                LoadingScreen.Instance.SetSliderValue(loadingOperation.progress);
            }

            yield return null;
        }

        LoadingScreen.Instance.SetPanelActive(false);
    }
}
