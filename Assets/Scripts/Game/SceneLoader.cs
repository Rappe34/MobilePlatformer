using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagement
{
    public class SceneLoader : MonoBehaviour
    {
        public AsyncOperation loadingOperation { get; private set; }
        public float progress { get; private set; } = 0f;

        private IEnumerator LoadScene(string sceneName)
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

        }
    }
}
