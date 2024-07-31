using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject playerCanvas; // PlayerCanvas'ý buraya baðla
    private bool isPaused = false;

    void Start()
    {
        // Oyun baþladýðýnda pause menüsünü gizle
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
        Debug.Log("PauseMenu scripti baþladý ve menü gizlendi"); // Hata ayýklama için ekleyin
    }

    void Update()
    {
        // ESC tuþuna basýldýðýnda menüyü aç/kapat
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC tuþuna basýldý"); // Hata ayýklama için ekleyin
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
        Debug.Log("Oyuna devam ediliyor"); // Hata ayýklama için ekleyin
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
        if (playerCanvas != null)
        {
            playerCanvas.SetActive(true); // PlayerCanvas'ý göster
        }
        Time.timeScale = 1f;  // Oyunu devam ettir
        isPaused = false;
        Debug.Log("Oyun devam ediyor, isPaused: " + isPaused); // Hata ayýklama için ekleyin
    }

    void Pause()
    {
        Debug.Log("Oyun duraklatýlýyor"); // Hata ayýklama için ekleyin
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
        }
        if (playerCanvas != null)
        {
            playerCanvas.SetActive(false); // PlayerCanvas'ý gizle
        }
        Time.timeScale = 0f;  // Oyunu duraklat
        isPaused = true;
        Debug.Log("Oyun duraklatýldý, isPaused: " + isPaused); // Hata ayýklama için ekleyin
    }
}
