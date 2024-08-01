using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToCheckpoint : MonoBehaviour
{
    public Image eButton;
    public Transform checkPoint;
    private GameObject player;
    private bool playerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        //eButton.gameObject.SetActive(false); // Ba�lang��ta E butonu g�r�n�r olmas�n
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            player.transform.position = checkPoint.position; // Player'� checkpoint'e ���nla
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            playerInRange = true;
            eButton.gameObject.SetActive(true); // E butonunu g�r�n�r yap
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            eButton.gameObject.SetActive(false); // E butonunu g�r�nmez yap
        }
    }
}

