using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResolutionSettings : MonoBehaviour
{
    public Dropdown resolutionDropdown;

    Resolution[] resolutions;

    void Start()
    {
        // Tüm çözünürlükleri al
        resolutions = Screen.resolutions;

        // Dropdown'u temizle
        resolutionDropdown.ClearOptions();

        // Dropdown seçenekleri için bir liste oluþtur
        List<string> options = new List<string>();

        // Mevcut çözünürlüðü bulmak için bir deðiþken oluþtur
        int currentResolutionIndex = 0;

        // Tüm çözünürlükleri seçeneklere ekle
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // Mevcut çözünürlüðü bul
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // Dropdown'a seçenekleri ekle
        resolutionDropdown.AddOptions(options);
        // Mevcut çözünürlüðü seçili hale getir
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Dropdown deðer deðiþikliði için bir listener ekle
        resolutionDropdown.onValueChanged.AddListener(delegate { SetResolution(resolutionDropdown.value); });
    }

    public void SetResolution(int resolutionIndex)
    {
        // Seçilen çözünürlüðü ayarla
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
