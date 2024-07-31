using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // "LoadingScene" sahnesini yüklemek için çaðrýlacak fonksiyon
    public void StartGame()
    {
        SceneManager.LoadScene("LoadingScene");
    }
}
