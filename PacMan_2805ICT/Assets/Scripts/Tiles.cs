using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    public bool Is_Energizer = false;
    public bool Is_Pellet = true;
    private bool Is_Eaten = false;
    public bool Is_Portal = false;
    public bool GhostHouseDoor = false;

    public GameObject PortalTo;
    


    public void setEaten(bool set)
    {
        Is_Eaten = set;
    }

    public bool getEaten()
    {
        return Is_Eaten;
    }

    
}
