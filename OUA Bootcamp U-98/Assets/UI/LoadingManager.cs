using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider progressBar;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        // Y�kleme ekran�n� g�ster
        loadingScreen.SetActive(true);

        // Sahneyi asenkron olarak y�kle
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // Sahne y�klenirken y�kleme ilerlemesini g�ncelle
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;
            yield return null;
        }
    }
}
