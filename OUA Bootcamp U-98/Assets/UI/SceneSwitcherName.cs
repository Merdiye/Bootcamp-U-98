using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcherName : MonoBehaviour
{
    // "UI Start" adlý sahneyi yükler
    public void LoadUIScene()
    {
        Debug.Log("LoadUIScene metodu çaðrýldý.");

        // Sahne adýnýn geçerli olduðunu ve sahnenin Build Settings'e eklendiðini kontrol et
        if (Application.CanStreamedLevelBeLoaded("UI Start"))
        {
            Debug.Log("Sahne 'UI Start' bulunuyor. Yükleniyor...");
            SceneManager.LoadScene("UI Start");
        }
        else
        {
            Debug.LogError("Sahne 'UI Start' bulunamadý veya Build Settings'e eklenmemiþ.");
        }
    }
}
