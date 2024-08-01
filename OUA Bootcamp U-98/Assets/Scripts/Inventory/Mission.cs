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

    public string missionString; // Editörde ayarlayacaðýnýz cümle

    private void Awake()
    {
        currentKill = 0f;
    }

    public void addKill()
    {
        currentKill++;
        currentKillText.text = currentKill.ToString();
    }

    // Bu fonksiyon çaðrýldýðýnda missionString deðerini missionText'e aktarýr
    public void SetMissionText()
    {
        targetKillText.text = targetKill.ToString();
        missionText.text = missionString;
    }
}

