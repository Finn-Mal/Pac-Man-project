﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletScript : MonoBehaviour
{
    public bool Is_Energizer = false;
    private bool Is_Eaten = false;


    public void setEaten(bool set)
    {
        Is_Eaten = set;
    }

    public bool checkEaten()
    {
        return Is_Eaten;
    }

}
