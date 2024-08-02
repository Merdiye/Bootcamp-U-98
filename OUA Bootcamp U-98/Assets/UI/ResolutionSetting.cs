using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResolutionSettings : MonoBehaviour
{
    public Dropdown resolutionDropdown;

    Resolution[] resolutions;

    void Start()
    {
        // T�m ��z�n�rl�kleri al
        resolutions = Screen.resolutions;

        // Dropdown'u temizle
        resolutionDropdown.ClearOptions();

        // Dropdown se�enekleri i�in bir liste olu�tur
        List<string> options = new List<string>();

        // Mevcut ��z�n�rl��� bulmak i�in bir de�i�ken olu�tur
        int currentResolutionIndex = 0;

        // T�m ��z�n�rl�kleri se�eneklere ekle
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // Mevcut ��z�n�rl��� bul
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // Dropdown'a se�enekleri ekle
        resolutionDropdown.AddOptions(options);
        // Mevcut ��z�n�rl��� se�ili hale getir
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Dropdown de�er de�i�ikli�i i�in bir listener ekle
        resolutionDropdown.onValueChanged.AddListener(delegate { SetResolution(resolutionDropdown.value); });
    }

    public void SetResolution(int resolutionIndex)
    {
        // Se�ilen ��z�n�rl��� ayarla
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}