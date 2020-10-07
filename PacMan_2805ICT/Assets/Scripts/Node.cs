using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Node Script from Chris Fletcher The Weekly Coder Youtube Tutorial

public class Node : MonoBehaviour
{
    // creating arrays to store neighbour nodes and valid [x,y] direction for the node to the neighbour
    public Node[] Neighbours;
    public Vector2[] valid_Direction;

    public bool travelPoint = false;

    // Start is called before the first frame update
    void Start()
    {
        // initalise the the potential valid Direction with length of Neighbours associated with that node (i.e if 2 neighbours, there are only 2 valid directions for that node)
        valid_Direction = new Vector2[Neighbours.Length];

        // initalise new neighbour variable which represent 1 neighbour with Neighbours
        for (int i = 0; i < Neighbours.Length; i++)
        {
            Node neighbour = Neighbours[i];

            // Create a vector between 1 neighbour and the current node to store (standarized) in valid_Direction
            Vector2 tempVector = neighbour.transform.localPosition - transform.localPosition;

            valid_Direction[i] = tempVector.normalized;

        }


        if (travelPoint)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }

    }


    
}
