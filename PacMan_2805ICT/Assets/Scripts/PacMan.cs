using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PacMan : MonoBehaviour
{
    //

    private bool CanMove = false;

    public float move_speed = 4.0f;

    private Node currentNode, nextNode, previousNode; // node of Pacman's current position (which pellet Node is he on)

    private Vector2 direction, nextDirection = Vector2.zero;


    // Start is called before the first frame update
    void Start()
    {

        Node PacmanNode = GetThePositionAtNode(transform.position); // Greating local node position of Pac Man in relation to all the Board
        
        if (PacmanNode != null)
        {
            currentNode = PacmanNode;
        }
        
        direction = Vector2.left;
        changeTargetNode(direction);
    }

    // Update is called once per frame
    void Update()
    {

        readInput();
        
        movePacman();
        
        updateOrientation();

        Debug.Log("current node: " + currentNode + " next node: " + nextNode + " previous node: " + previousNode);
        
    }



    protected void readInput()
    {
        //function to read user input and change direction of the Pac-Man

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {

            changeTargetNode(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {

            changeTargetNode(Vector2.right);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {

            changeTargetNode(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {

            changeTargetNode(Vector2.left);
        }
    }


    void changeTargetNode(Vector2 newDirection)
    {
        if(newDirection != direction)
        {
            nextDirection = newDirection;
        }

        

        if(currentNode != null)
        {
            Node moveToNode = ValidMove(newDirection);
            if(moveToNode != null)
            {
                direction = newDirection;
                nextNode = moveToNode;
                previousNode = currentNode;
                currentNode = null;
            }
        }
    }


    void movePacman()
    {
        if (nextNode != currentNode && nextNode != null)
        {
            if (positionOvershot())
            {

                currentNode = nextNode;
                transform.localPosition = currentNode.transform.position;

                Node moveToNode = ValidMove(nextDirection);
                if(moveToNode!= null)
                {
                    direction = nextDirection;

                }
                if(moveToNode == null)
                {
                    moveToNode = ValidMove(direction);
                    
                }
                if(moveToNode != null)
                {
                    nextNode = moveToNode;
                    previousNode = currentNode;
                    currentNode = null;
                } else
                {
                    direction = Vector2.zero;
                }
            }
            else
            {
                transform.position += (Vector3)(direction * this.move_speed) * Time.deltaTime;
            }
        }
        
    }


    //update pacman's UI orientation and animate him "moving" in a set vector direction per frame
    void updateOrientation()
    {
        if (direction == Vector2.up)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90); 

        }
        else if (direction == Vector2.right)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            
        }
        else if (direction == Vector2.down)
        {
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        else if (direction == Vector2.left)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        
    }


    

    //check if the direction give by player correlates with direction of a node
    Node ValidMove(Vector2 direction)
    {
        Node NextNode = null;
        for (int i = 0; i < currentNode.Neighbours.Length; i++)
        {
            if (currentNode.valid_Direction[i] == direction)
            {
                NextNode = currentNode.Neighbours[i];
                break;
            }
        }

        return NextNode;
    }


    //update pacman's poition via node to node movment
    void nodeMovement(Vector2 direction)
    {
        Node movetoNode = ValidMove(direction);

        if (movetoNode != null)
        {
           transform.localPosition = movetoNode.transform.position;
           currentNode = movetoNode;
        }
    }

    Node GetThePositionAtNode(Vector2 position)
    {


        // Creating a game object within Pac Man script to call reference to squareBoard GameObject within GameBoard (specifically the GameObject stored at position)
        GameObject tile = GameObject.Find("GameBoard").GetComponent<The_Board>().Board[(int)position.x, (int)position.y];
        if (tile != null)
        {
            return tile.GetComponent<Node>(); //returns the tile Node that PacMan is currently located on

        }

        return null;
    }

    bool positionOvershot()
    {
        float nodetoTarget = lengthFromNode(nextNode.transform.position); // return the length target node to previous node
        float nodetoPacman = lengthFromNode(transform.localPosition); // return length of pac man from previous node

        return nodetoPacman > nodetoTarget; // return if pacman has travel further distance then targetnode - previousnode
    }


    float lengthFromNode(Vector2 targetPosition)
    {
        Vector2 vec = targetPosition - (Vector2)previousNode.transform.position;
        return vec.sqrMagnitude;
    }

}
