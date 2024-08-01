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
        //eButton.gameObject.SetActive(false); // Baþlangýçta E butonu görünür olmasýn
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            player.transform.position = checkPoint.position; // Player'ý checkpoint'e ýþýnla
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            playerInRange = true;
            eButton.gameObject.SetActive(true); // E butonunu görünür yap
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            eButton.gameObject.SetActive(false); // E butonunu görünmez yap
        }
    }
}

