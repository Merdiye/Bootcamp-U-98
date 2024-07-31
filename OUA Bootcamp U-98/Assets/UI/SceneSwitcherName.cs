using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcherName : MonoBehaviour
{
    // "UI Start" adl� sahneyi y�kler
    public void LoadUIScene()
    {
        // Sahne ad�n�n ge�erli oldu�unu ve sahnenin Build Settings'e eklendi�ini kontrol et
        if (SceneManager.GetSceneByName("UI Start").IsValid())
        {
            SceneManager.LoadScene("UI Start");
        }
        else
        {
            Debug.LogError("Sahne 'UI Start' bulunamad� veya Build Settings'e eklenmemi�.");
        }
    }
}
