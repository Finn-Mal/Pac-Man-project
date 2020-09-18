using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletScript : MonoBehaviour
{
    public bool Is_Energizer = false;

    private GameObject The_Pacman;
    // Start is called before the first frame update
    void Start()
    {
        The_Pacman = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Pellet_Eaten();
    }


    void Pellet_Eaten()
    {
        if (transform.position == The_Pacman.transform.position)
        {
            GetComponent<Renderer>().enabled = false;
        }
    }
}
