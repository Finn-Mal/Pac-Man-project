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

    public float move_speed = 4.0f;


    // Start is called before the first frame update
    void Start()
    {
        
        currentDirection = Direction.None;
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
       }
       else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
       {
           currentDirection = Direction.East;
       }
       else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
       {
           currentDirection = Direction.South;
       }
       else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
       {
           currentDirection = Direction.West;
       }
    }

    protected void updateMovement()
    {
        switch(this.currentDirection)
        {
            case Direction.North:
                {
                    transform.position += (Vector3)(Vector2.up * this.move_speed) * Time.deltaTime;

                    transform.rotation = Quaternion.Euler(0, 0, 90);

                    break;
                }
            case Direction.East:
                {
                    transform.position += (Vector3)(Vector2.right * this.move_speed) * Time.deltaTime;
                    
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                }
            case Direction.South:
                {
                    transform.position += (Vector3)(Vector2.down * this.move_speed) * Time.deltaTime;
                    
                    transform.rotation = Quaternion.Euler(0, 0, 270);
                    break;
                }
            case Direction.West:
                {
                    transform.position += (Vector3)(Vector2.left * this.move_speed) * Time.deltaTime;
                    
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                    break;
                }

        }
    }

}
