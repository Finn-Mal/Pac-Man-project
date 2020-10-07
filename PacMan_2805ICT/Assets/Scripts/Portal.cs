using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    public GameObject teleportTo;
    public bool isPortal = false;


    void Teleport()
    {
        GameObject ThePacman = GameObject.FindWithTag("Player");
    }
}
