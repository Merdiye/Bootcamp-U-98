using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // "LoadingScene" sahnesini y�klemek i�in �a�r�lacak fonksiyon
    public void StartGame()
    {
        SceneManager.LoadScene("LoadingScene");
    }
}
