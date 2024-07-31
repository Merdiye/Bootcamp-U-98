using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance { get; private set; }
    public GameObject loadingScreen; // Yükleme ekraný (Canvas)
    public Slider progressBar;       // Yükleme ilerleme çubuðu
    public float minimumLoadingTime = 2.0f; // Minimum bekleme süresi

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Bu GameObject'i sahne deðiþtirirken yok etme
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // "ANA SAHNE" sahnesini yüklemek için çaðrýlacak fonksiyon
    private void Start()
    {
        StartCoroutine(LoadSceneAsync("ANA SAHNE"));
    }

    // Asenkron sahne yükleme iþlemi
    IEnumerator LoadSceneAsync(string sceneName)
    {
        // Eðer `loadingScreen` aktif deðilse aktif et
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(true);
        }

        // Sahneyi asenkron olarak yükle
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        // Sahne yüklenirken yükleme ilerlemesini güncelle
        float elapsedTime = 0f;
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            if (progressBar != null)
            {
                progressBar.value = progress;
            }

            elapsedTime += Time.deltaTime;

            // Minimum bekleme süresi dolana kadar sahne geçiþini beklet
            if (operation.progress >= 0.9f && elapsedTime >= minimumLoadingTime)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
