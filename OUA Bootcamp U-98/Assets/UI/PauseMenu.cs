using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject playerCanvas; // PlayerCanvas'� buraya ba�la
    private bool isPaused = false;

    void Start()
    {
        // Oyun ba�lad���nda pause men�s�n� gizle
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
        Debug.Log("PauseMenu scripti ba�lad� ve men� gizlendi"); // Hata ay�klama i�in ekleyin
    }

    void Update()
    {
        // ESC tu�una bas�ld���nda men�y� a�/kapat
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC tu�una bas�ld�"); // Hata ay�klama i�in ekleyin
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Debug.Log("Oyuna devam ediliyor"); // Hata ay�klama i�in ekleyin
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
        if (playerCanvas != null)
        {
            playerCanvas.SetActive(true); // PlayerCanvas'� g�ster
        }
        Time.timeScale = 1f;  // Oyunu devam ettir
        isPaused = false;
        Debug.Log("Oyun devam ediyor, isPaused: " + isPaused); // Hata ay�klama i�in ekleyin
    }

    void Pause()
    {
        Debug.Log("Oyun duraklat�l�yor"); // Hata ay�klama i�in ekleyin
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
        }
        if (playerCanvas != null)
        {
            playerCanvas.SetActive(false); // PlayerCanvas'� gizle
        }
        Time.timeScale = 0f;  // Oyunu duraklat
        isPaused = true;
        Debug.Log("Oyun duraklat�ld�, isPaused: " + isPaused); // Hata ay�klama i�in ekleyin
    }
}
