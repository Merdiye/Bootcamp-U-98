using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject playerCanvas; // PlayerCanvas'ý buraya baðla
    private bool isPaused = false;

    void Update()
    {
        // ESC tuþuna basýldýðýnda menüyü aç/kapat
        if (Input.GetKeyDown(KeyCode.Escape))
        {
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
        pauseMenuUI.SetActive(false);
        playerCanvas.SetActive(true); // PlayerCanvas'ý göster
        Time.timeScale = 1f;  // Oyunu devam ettir
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        playerCanvas.SetActive(false); // PlayerCanvas'ý gizle
        Time.timeScale = 0f;  // Oyunu duraklat
        isPaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();  // Oyundan çýk
    }
}

