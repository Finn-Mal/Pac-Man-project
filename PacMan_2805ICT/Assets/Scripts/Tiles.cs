using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    public bool Is_Energizer = false;
    public bool Is_Pellet = true;
    private bool Is_Eaten = false;


    public GameObject PortalTo;
    public bool Is_Portal = false;


    public void setEaten(bool set)
    {
        Is_Eaten = set;
    }

    public bool checkEaten()
    {
        return Is_Eaten;
    }

    
}
