using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeSkill : MonoBehaviour
{
    public bool isDodging;
    [SerializeField] GameObject image;

    private void Awake()
    {
        isDodging = false;
    }




}
