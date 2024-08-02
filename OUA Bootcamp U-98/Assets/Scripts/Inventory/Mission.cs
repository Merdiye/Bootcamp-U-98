using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Mission : MonoBehaviour
{
    public static Mission Instance;
    public TextMeshProUGUI missionText; // TextMeshPro nesnesi
    private float currentKill;
    public float targetKill;
    public TextMeshProUGUI targetKillText;
    public TextMeshProUGUI currentKillText;


    public string missionString; // Edit�rde ayarlayaca��n�z c�mle

    private void Awake()
    {
      

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        currentKill = 0f;

     
  
    }

    public void addKill()
    {
        currentKill++;
        currentKillText.text = currentKill.ToString();
        if (currentKill >= targetKill)
        {
            Debug.Log("Görev Tamamlandı!");
         
        }
    }

    // Bu fonksiyon �a�r�ld���nda missionString de�erini missionText'e aktar�r
    public void SetMissionText()
    {
        targetKillText.text = targetKill.ToString();
        missionText.text = missionString;
    }
}

