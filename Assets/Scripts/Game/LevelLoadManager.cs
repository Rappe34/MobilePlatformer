using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoadManager : MonoBehaviour
{
    public static LevelLoadManager Instance;

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

    public void LoadLevel(LevelDataSO levelData)
    {
        StartCoroutine(LoadLevel_(levelData));
    }

    private IEnumerator LoadLevel_(LevelDataSO levelData)
    {
        yield return StartCoroutine(SceneLoader.Instance.LoadScene(levelData.levelSceneName));

        GameManager.Instance.StartLevel(levelData);
    }
}
