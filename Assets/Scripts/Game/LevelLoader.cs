using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance;

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
        yield return StartCoroutine(SceneLoader.Instance.LoadScene(levelData));

        GameManager.Instance.StartLevel(levelData);
    }
}
