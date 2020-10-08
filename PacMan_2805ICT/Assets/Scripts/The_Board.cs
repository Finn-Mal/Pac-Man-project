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
    private GameObject[] All_Portals;
    private GameObject[] All_Tiles;

    private int GameScore = 0;
    private int pelletsRemain = 0;


    private GameObject ThePacman;


    // Start is called before the first frame update
    void Start()
    {
        
        All_Tiles= GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject Single_Tiles in All_Tiles)
        {
            
            Vector2 position = Single_Tiles.transform.position;
            Board[(int)position.x, (int)position.y] = Single_Tiles;

        }

        
        ThePacman = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        MonitorGameboard();

        GameStatus();
    }

    void MonitorGameboard()
    {
        GameObject tile = CheckTile();

        if (tile != null)
        {

            Tiles Tilecomponent = tile.GetComponent<Tiles>();//from the tile gameobject, retrieve the tile script that is attached to it 


            if (Tilecomponent != null )
            {

                if (Tilecomponent.Is_Pellet || Tilecomponent.Is_Energizer)
                {
                    tile.GetComponent<SpriteRenderer>().enabled = false;
                    Tilecomponent.setEaten(true);
                    GameScore += 10;
                    pelletsRemain += 1;
                }

                
            }


        }
    }




    GameObject CheckTile()
    {
        int PacX = Mathf.RoundToInt(ThePacman.transform.position.x);
        int PacY = Mathf.RoundToInt(ThePacman.transform.position.y);

        GameObject tile = Board[PacX, PacY];
        if (tile != null)
        {
            return tile;
        }

        return null;
    }


    void GameStatus()
    {
        if(pelletsRemain == 225)
        {

        }
    }

    void loadNextLevel()
    {

    }
}
