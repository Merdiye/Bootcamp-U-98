using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance { get; private set; }
    public GameObject loadingScreen; // Y�kleme ekran� (Canvas)
    public Slider progressBar;       // Y�kleme ilerleme �ubu�u
    public float minimumLoadingTime = 2.0f; // Minimum bekleme s�resi

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Bu GameObject'i sahne de�i�tirirken yok etme
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // "ANA SAHNE" sahnesini y�klemek i�in �a�r�lacak fonksiyon
    private void Start()
    {
        StartCoroutine(LoadSceneAsync("ANA SAHNE"));
    }

    // Asenkron sahne y�kleme i�lemi
    IEnumerator LoadSceneAsync(string sceneName)
    {
        // E�er `loadingScreen` aktif de�ilse aktif et
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(true);
        }

        // Sahneyi asenkron olarak y�kle
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        // Sahne y�klenirken y�kleme ilerlemesini g�ncelle
        float elapsedTime = 0f;
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            if (progressBar != null)
            {
                progressBar.value = progress;
            }

            elapsedTime += Time.deltaTime;

            // Minimum bekleme s�resi dolana kadar sahne ge�i�ini beklet
            if (operation.progress >= 0.9f && elapsedTime >= minimumLoadingTime)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
