using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
                                                      // Sahneyi ad ile y�kle
    public void LoadSceneByName()
    {
        SceneManager.LoadScene("ANA SAHNE");
    }
}


