using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class The_Board : MonoBehaviour
{
    //setting grid width and height for pacman game

    private static int boardWidth = 28;
    private static int boardHeight = 36; // 28 x 36 is the standard pacman board size

    // an array to store the X, Y cooridinates of a game object currently in the scene (maze Pixels, Pellets, PacMan etc
    public GameObject[,] squareBoard = new GameObject[boardWidth, boardHeight];

    // Start is called before the first frame update
    void Start()
    {

        // an array the will store all objects (declared as GameObjects) that are present in current scene 
        Object[] boardObjects = GameObject.FindObjectsOfType(typeof(GameObject));

        foreach (GameObject singleObject in boardObjects)
        {
            // for each object found within the board, locate it's individual position
            Vector2 position = singleObject.transform.position;
            //Debug.Log(singleObject + " " + singleObject.transform.position);

            if (singleObject.name != "PacMan") // to ensure pacman's position is stored seperately from the board
            {

                // + 1 is there as (int)position reduce position but 1 unit (i.e [15,7] becomes [14,6]
                //squareBoard[(int)position.x + 1, (int)position.y + 1] = singleObject;

                squareBoard[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y)] = singleObject;

                Debug.Log(squareBoard[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y)] + " is being sorted in x " + (int)position.x + " y " + (int)position.y);
       
            }
        }




        /*GameObject[] boardObjects = GameObject.FindGameObjectsWithTag("Node_Pellet");

        foreach (GameObject singleObject in boardObjects)
        {
            Debug.Log(singleObject.transform.position);
            // for each object found within the board, locate it's individual position
            Vector2 position = singleObject.transform.position;

            squareBoard[(int)position.x, (int)position.y] = singleObject;

            
        }*/



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
