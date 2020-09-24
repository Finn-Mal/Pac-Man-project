using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class The_Board : MonoBehaviour
{
    //setting grid width and height for pacman game

    private static int boardWidth = 28;
    private static int boardHeight = 36; // 28 x 36 is the standard pacman board size

    // an array to store the X, Y cooridinates of a game object currently in the scene (maze Pixels, Pellets, PacMan etc
    public GameObject[,] Board = new GameObject[boardWidth, boardHeight];
    private GameObject[] All_Pellets;

    // Start is called before the first frame update
    void Start()
    {
        All_Pellets = GameObject.FindGameObjectsWithTag("Pellets");
        
        foreach(GameObject Single_Pellet in All_Pellets)
        {
            Vector2 position = Single_Pellet.transform.position;
            Board[(int)position.x, (int)position.y] = Single_Pellet;
            
        }
        



            /*// an array the will store all objects (declared as GameObjects) that are present in current scene 
        Object[] boardObjects = GameObject.FindObjectsOfType(typeof(GameObject));

        foreach (GameObject singleObject in boardObjects)
        {
            Debug.Log(singleObject + " position is " + singleObject.transform.position);
            // for each object found within the board, locate it's individual position
            Vector2 position = singleObject.transform.position;
            //Debug.Log(singleObject + " " + singleObject.transform.position);

            if (singleObject.name != "PacMan") // to ensure pacman's position is stored seperately from the board
            {
                //squareBoard[(int)position.x, (int)position.y] = singleObject;

            }
            
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
