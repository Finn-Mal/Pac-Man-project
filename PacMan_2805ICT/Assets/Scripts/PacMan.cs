using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PacMan : MonoBehaviour
{
    // enum to store state of Pac-Man direction
    // None being initial stand still state;
    private enum Direction {None, North, East, South, West };
    private Direction currentDirection;

    private bool CanMove = false;

    public float move_speed = 4.0f;

    private Node currentNode; // node of Pacman's current position (which pellet Node is he on)




    // Start is called before the first frame update
    void Start()
    {
        
        currentDirection = Direction.None;

        Node PacmanNode = GetThePositionAtNode(transform.position); // Greating local node position of Pac Man in relation to all the Board

        if (PacmanNode != null)
        {
            currentNode = PacmanNode;
            Debug.Log(currentNode);
        }
    }

    // Update is called once per frame
    void Update()
    {
        readInput();
        updateMovement();
    }



    protected void readInput()
    {
        //function to read user input and change direction of the Pac-Man

       if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
       {
           currentDirection = Direction.North;
           nodeMovement(Vector2.up);
       }
       else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
       {
           currentDirection = Direction.East;
           nodeMovement(Vector2.right);
        }
       else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
       {
           currentDirection = Direction.South;
           nodeMovement(Vector2.down);
       }
       else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
       {
           currentDirection = Direction.West;
           nodeMovement(Vector2.left);
       }
    }




    Node ValidMove (Vector2 direction)
    {
        Node NextNode = null;
        for(int i = 0; i < currentNode.Neighbours.Length; i++)
        {
            if (currentNode.valid_Direction[i] == direction)
            {
                NextNode = currentNode.Neighbours[i];
                break;
            }
        }

        return NextNode;
    }

    protected void updateMovement()
    {
        switch(this.currentDirection)
        {
            case Direction.North:
                {
                    //transform.position += (Vector3)(Vector2.up * this.move_speed) * Time.deltaTime;

                    transform.rotation = Quaternion.Euler(0, 0, 90);

                    break;
                }
            case Direction.East:
                {
                    //transform.position += (Vector3)(Vector2.right * this.move_speed) * Time.deltaTime;
                    
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                }
            case Direction.South:
                {
                    //transform.position += (Vector3)(Vector2.down * this.move_speed) * Time.deltaTime;
                    
                    transform.rotation = Quaternion.Euler(0, 0, 270);
                    break;
                }
            case Direction.West:
                {
                    //transform.position += (Vector3)(Vector2.left * this.move_speed) * Time.deltaTime;
                    
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                    break;
                }

        }
    }


    void nodeMovement (Vector2 direction)
    {
        Node movetoNode = ValidMove(direction);

        if (movetoNode != null)
        {
            transform.localPosition = movetoNode.transform.position;
            currentNode = movetoNode;
        }
    }

    Node GetThePositionAtNode (Vector2 position)
    {
        

        // Creating a game object within Pac Man script to call reference to squareBoard GameObject within GameBoard (specifically the GameObject stored at position)
        GameObject tile = GameObject.Find("GameBoard").GetComponent<The_Board>().Board[(int)position.x, (int)position.y];
        if (tile != null)
        {
            return tile.GetComponent<Node>(); //returns the tile Node that PacMan is currently located on
            
        }

        return null;
    }

}
