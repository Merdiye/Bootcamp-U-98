using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepScript : MonoBehaviour
{
    public GameObject WoodFootsteps;
    public GameObject GrassFootsteps;

    private void Start()
    {
        WoodFootsteps.SetActive(false);
        GrassFootsteps.SetActive(false);
    }

    void Update()
    {
        string surfaceTag = GetSurfaceTag();

        if(surfaceTag == "Wood")
        {
            if (Input.GetKeyDown("w"))
            {
                Woodfootsteps();
            }

            if (Input.GetKeyDown("s"))
            {
                Woodfootsteps();
            }

            if (Input.GetKeyDown("a"))
            {
                Woodfootsteps();
            }

            if (Input.GetKeyDown("d"))
            {
                Woodfootsteps();
            }

            if (Input.GetKeyUp("w"))
            {
                StopWoodFootsteps();
            }

            if (Input.GetKeyUp("s"))
            {
                StopWoodFootsteps();
            }

            if (Input.GetKeyUp("a"))
            {
                StopWoodFootsteps();
            }

            if (Input.GetKeyUp("d"))
            {
                StopWoodFootsteps();
            }
        }

        if (surfaceTag == "grass")
        {
            if (Input.GetKeyDown("w"))
            {
                Grassfootsteps();
            }

            if (Input.GetKeyDown("s"))
            {
                Grassfootsteps();
            }

            if (Input.GetKeyDown("a"))
            {
                Grassfootsteps();
            }

            if (Input.GetKeyDown("d"))
            {
                Grassfootsteps();
            }

            if (Input.GetKeyUp("w"))
            {
                StopGrassFootsteps();
            }

            if (Input.GetKeyUp("s"))
            {
                StopGrassFootsteps();
            }

            if (Input.GetKeyUp("a"))
            {
                StopGrassFootsteps();
            }

            if (Input.GetKeyUp("d"))
            {
                StopGrassFootsteps();
            }
        }



    }


    void Woodfootsteps()
    {
        WoodFootsteps.SetActive(true);
    }

    void StopWoodFootsteps()
    {
        WoodFootsteps.SetActive(false);
    }

    void Grassfootsteps()
    {
        GrassFootsteps.SetActive(true);
    }

    void StopGrassFootsteps()
    {
        GrassFootsteps.SetActive(false);
    }


    string GetSurfaceTag()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
        {
            string tag = hit.collider.tag;
            Debug.Log("Detected Tag: " + tag); // Tag’in doğru alındığını kontrol edin
            return tag;
        }

        Debug.Log("No Tag Detected");
        return "Unknown";
    }
}


