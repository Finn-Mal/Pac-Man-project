using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    public float movespeed = 3.9f; //move speed of Ghost to pacman (always slightly slow in first couple levels

    public Node StartNode; //first node for Ghost to travel too

    public int scatterModePhase = 7;
    public int chaseModePhase = 20;
    public int scatterModePhaseHard = 5;
    public int currentscatterModePhase;


    public int stateIteration = 1;
    private float stateTimer = 0;

    public enum Mode {Chase, Scatter, Frighten}

    Mode currentMode = Mode.Scatter;
    Mode previousMode;

    private GameObject Pacman;


    private Node currentNode, nextNode, previousNode;
    private Vector2 currectDirection, nextDirection;


    // Start is called before the first frame update
    void Start()
    {
        Pacman = GameObject.FindGameObjectWithTag("Player");

        Node initialNode = GetNodePosition(transform.localPosition);

        if (initialNode != null)
        {
            currentNode = initialNode;
        }
        previousNode = currentNode;
        currentscatterModePhase = scatterModePhase;

        Vector2 targetTile = Vector2.zero; //Vector that will define the tile X Y position Pacman is in
        targetTile = new Vector2(Mathf.RoundToInt(Pacman.transform.position.x), Mathf.RoundToInt(Pacman.transform.position.y));
        nextNode = GetNodePosition(targetTile);

        currectDirection = Vector2.left; //for testing ghost
    }

    // Update is called once per frame
    void Update()
    {
        modeUpdate();

        moveGhost();

    }

    void moveGhost()
    {
        if (nextNode != currentNode && nextNode != null) // if next ndoe ghost trevels exists and isn't current target
        {

            if (positionOvershot()) //if they have overshot that position of the node
            {

                currentNode = nextNode;
                transform.localPosition = currentNode.transform.position;

                GameObject Portaldestination = GetPortal(currentNode.transform.position);
                if (Portaldestination != null)
                {
                    transform.localPosition = Portaldestination.transform.position;

                    currentNode = Portaldestination.GetComponent<Node>();
                }

                nextNode = SelecteNextNode();
                previousNode = currentNode;
                currentNode = null;

                Debug.Log(nextNode);
            }
            else
            {
                transform.localPosition += (Vector3)currectDirection * movespeed * Time.deltaTime;
            }
        }
        
    }


    void modeUpdate()
    {
        if (currentMode != Mode.Frighten)
        {
            stateTimer += Time.deltaTime;
            if(stateIteration < 4)
            {
                if(currentMode == Mode.Scatter && stateTimer > currentscatterModePhase)
                {
                    changeMode(Mode.Chase);
                    stateTimer = 0f;
                }
                if(currentMode == Mode.Chase && stateTimer > chaseModePhase)
                {
                    
                    changeMode(Mode.Scatter);
                    stateTimer = 0f;
                }
            }
            else
            {   stateIteration++;
                changeMode(Mode.Chase);
            }
            
            if (stateIteration > 2)
            {
                currentscatterModePhase = scatterModePhaseHard;
            }

            //Debug.Log(currentMode);
        }

        else
        {

        }
    }

    void changeMode (Mode newMode)
    {
        currentMode = newMode;
    }




    void FrightenModeActivate()
    {

    }


    Node SelecteNextNode()
    {
        Vector2 targetTile = Vector2.zero; //Vector that will define the tile X Y position Pacman is in
        targetTile = new Vector2(Mathf.RoundToInt(Pacman.transform.position.x),Mathf.RoundToInt(Pacman.transform.position.y));

        
        Node moveToNode = null;

        Node[] foundNodes = new Node[4];
        Vector2[] foundNodesDirection = new Vector2[4]; //from current position, array of one of four nodes Ghost can move too

        int nodeCounter = 0;

        for (int i = 0; i < currentNode.Neighbours.Length; i++)
        {
            if(currentNode.valid_Direction[i] != currectDirection * -1) // avoid ghost going in reverse
            {
                foundNodes[nodeCounter] = currentNode.Neighbours[i];
                foundNodesDirection[nodeCounter] = currentNode.valid_Direction[i];
                nodeCounter++; 
            }
        }

        //Debug.Log(targetTile + " " + Pacman.transform.position);

        if (foundNodes.Length == 1)
        {
            moveToNode = foundNodes[0];
            currectDirection = foundNodesDirection[0];
        }

        if(foundNodes.Length > 1)
        {
            float leastDistance = 9999f;

            for(int i = 0; i < foundNodes.Length; i++)
            {
                
                if (foundNodesDirection[i] != Vector2.zero)
                {
                    Debug.Log("Ghost: " + currentNode + " " + "Is going to: " + moveToNode);
                    Debug.Log("BEFORE: Potential next node" +  foundNodes[i] + " where pacman is " + targetTile);
                    float distance = MeasureDistance(foundNodes[i].transform.position, targetTile); // measures the distance between 

                    if(distance < leastDistance)
                    {
                        leastDistance = distance;
                        moveToNode = foundNodes[i];
                        currectDirection = foundNodesDirection[i];
                    }
                }
            }
        }
        
        return moveToNode;
    }







    //find at tile at Given ghost position and check if that tile has a Node component
    Node GetNodePosition(Vector2 position)
    {
        GameObject tile = GameObject.Find("GameBoard").GetComponent<The_Board>().Board[(int)position.x, (int)position.y];

        if (tile != null)
        {
            if(tile.GetComponent<Node>() != null)
            {
                return tile.GetComponent<Node>();
            }
        }

        return null;
    }

    GameObject GetPortal(Vector2 Position)
    {
        GameObject tile = GameObject.Find("GameBoard").GetComponent<The_Board>().Board[(int)Position.x, (int)Position.y];
        if (tile != null)
        {
            if (tile.GetComponent<Tiles>().Is_Portal)
            {
                GameObject nextPortal = tile.GetComponent<Tiles>().PortalTo;
                return nextPortal;
            }
        }

        return null;
    }

    


    float lengthFromNode(Vector2 targetPosition)
    {
        Vector2 vec = targetPosition - (Vector2)previousNode.transform.position;
        return vec.sqrMagnitude;
    }

    bool positionOvershot()
    {
        float nodetoTarget = lengthFromNode(nextNode.transform.position); // return the length target node to previous node
        float nodetoSelf = lengthFromNode(transform.localPosition); // return length of pac man from previous node

        return nodetoSelf > nodetoTarget; // return if pacman has travel further distance then targetnode - previousnode
    }

    float MeasureDistance(Vector2 posA, Vector2 posB)
    {

        Debug.Log("posA.x: " + posA.x + " posA.y " + posA.y + " posB.x " + posB.x + " posB.y " + posB.y);
        float dx = posA.x - posB.x;
        float dy = posA.y - posB.y;

        Debug.Log("dx: " + dx + " dy " + dy);
        float distance = Mathf.Sqrt(dx * dx + dy * dy); //pythagoras used to measure distance between ghost and Pacman
        Debug.Log("Distance: " + distance);

        return distance;
    }
}
