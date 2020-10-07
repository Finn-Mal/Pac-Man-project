using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanHex : MonoBehaviour
{
    private bool CanMove = false;

    public float move_speed = 4.0f;

    public Node initialNode;

    private Node currentNode, nextNode, previousNode; // node of Pacman's current position (which pellet Node is he on)

    private Vector2 direction, nextDirection;


    // Start is called before the first frame update
    void Start()
    {

        //Node PacmanNode = GetThePositionAtNode(transform.position); // Greating local node position of Pac Man in relation to all the Board

        /*if (PacmanNode != null)
        {
            currentNode = PacmanNode;
        }*/

        currentNode = initialNode;

        direction = new Vector2(0.7f, -0.7f);
        changeTargetNode(direction);
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(Input.GetAxisRaw("Horizontal") + " " + Input.GetAxisRaw("Vertical"));

        readInput();

        movePacman();

        updateOrientation();

    }



    protected void readInput()
    {
        //function to read user input and change direction of the Pac-Man
        Vector2 temp = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) // Up direction
        {
            changeTargetNode(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.D) && Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.RightArrow)) //diagonal Up Right direction
        {
            temp = new Vector2(0.7f, 0.7f);
            changeTargetNode(temp);
        }
        else if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.LeftArrow)) //diagonal Up left direction
        {
            temp = new Vector2(-0.7f, 0.7f);
            changeTargetNode(temp);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) // down direction
        {

            changeTargetNode(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.D) && Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftArrow)) //diagonal Down Right direction
        {
            temp = new Vector2(0.7f, -0.7f);
            changeTargetNode(temp);
        }
        else if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftArrow)) //diagonal Down Left direction
        {
            temp = new Vector2(-0.7f, -0.7f);
            changeTargetNode(temp);
        }
    }


    void changeTargetNode(Vector2 newDirection)
    {
        if (newDirection != direction)
        {
            nextDirection = newDirection;
        }


        if (currentNode != null)
        {
            Node moveToNode = ValidMove(newDirection);
            if (moveToNode != null)
            {
                

                direction = newDirection;
                nextNode = moveToNode;
                previousNode = currentNode;
                currentNode = null;


                
            }
        }

        if (currentNode == null)
        {
            if (Vector2.Dot(newDirection, direction) == -1)
            {
                direction = newDirection;
                Node swapNode = nextNode;
                nextNode = previousNode;
                previousNode = swapNode;
            }
        }

        Debug.Log("current Node: " + currentNode + " Next Node: " + nextNode + "previous node: " + previousNode);
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
                if (moveToNode != null)
                {
                    direction = nextDirection;

                }
                if (moveToNode == null)
                {
                    moveToNode = ValidMove(direction);

                }
                if (moveToNode != null)
                {
                    nextNode = moveToNode;
                    previousNode = currentNode;
                    currentNode = null;
                }
                else
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

            
            if (Mathf.Round(Vector2.Dot(currentNode.valid_Direction[i], direction)) == 1)
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
