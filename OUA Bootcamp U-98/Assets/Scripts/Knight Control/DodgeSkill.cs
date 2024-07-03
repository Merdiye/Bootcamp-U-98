using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DodgeSkill : MonoBehaviour
{
    public bool isDodging;
    public Image image;

    private void Awake()
    {
        image = FindObjectOfType<Image>();
        isDodging = false;
    }




}
