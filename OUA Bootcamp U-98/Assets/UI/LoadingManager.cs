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
        // Yükleme ekranýný göster
        loadingScreen.SetActive(true);

        // Sahneyi asenkron olarak yükle
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // Sahne yüklenirken yükleme ilerlemesini güncelle
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;
            yield return null;
        }
    }
}
