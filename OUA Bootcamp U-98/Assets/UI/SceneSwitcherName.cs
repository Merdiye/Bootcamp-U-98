using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcherName : MonoBehaviour
{
    // "UI Start" adlý sahneyi yükler
    public void LoadUIScene()
    {
        // Sahne adýnýn geçerli olduðunu ve sahnenin Build Settings'e eklendiðini kontrol et
        if (SceneManager.GetSceneByName("UI Start").IsValid())
        {
            SceneManager.LoadScene("UI Start");
        }
        else
        {
            Debug.LogError("Sahne 'UI Start' bulunamadý veya Build Settings'e eklenmemiþ.");
        }
    }
}
