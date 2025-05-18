using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen Instance { get; private set; }

    public string ActiveScene { get; private set; }

    [SerializeField] private Slider slider;

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

    private void Start()
    {
        ActiveScene = SceneManager.GetActiveScene().name;
    }

    public void LoadScene(string name)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(name);

        gameObject.SetActive(true);

        while (loading.progress < 0.9f)
        {
            SetSliderValue(loading.progress);
        }

        if (loading.isDone) SetSliderValue(1f);

        ActiveScene = name;

        gameObject.SetActive(false);
    }

    private void SetSliderValue(float value)
    {
        slider.value = value;
    }
}
