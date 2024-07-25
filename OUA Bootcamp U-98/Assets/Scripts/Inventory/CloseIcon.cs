using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseIcon : MonoBehaviour
{
    public void TimeToNormal()
    {
        Time.timeScale = 1.0f;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
}
