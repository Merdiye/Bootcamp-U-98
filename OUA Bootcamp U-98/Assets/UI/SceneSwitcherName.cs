using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcherName : MonoBehaviour
{
    // "UI Start" adl� sahneyi y�kler
    public void LoadUIScene()
    {
        Debug.Log("LoadUIScene metodu �a�r�ld�.");

        // Sahne ad�n�n ge�erli oldu�unu ve sahnenin Build Settings'e eklendi�ini kontrol et
        if (Application.CanStreamedLevelBeLoaded("UI Start"))
        {
            Debug.Log("Sahne 'UI Start' bulunuyor. Y�kleniyor...");
            SceneManager.LoadScene("UI Start");
        }
        else
        {
            Debug.LogError("Sahne 'UI Start' bulunamad� veya Build Settings'e eklenmemi�.");
        }
    }
}
