using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class The_Board : MonoBehaviour
{
    //setting grid width and height for pacman game

    private static int boardWidth = 28;
    private static int boardHeight = 36; // 28 x 36 is the standard pacman board size

    private float Timepassed = 0f;

    
    public Text HighScore;
    public Image[] LivesRemaining = new Image[3];

    // an array to store the X, Y cooridinates of a game object currently in the scene (maze Pixels, Pellets, PacMan etc
    public GameObject[,] Board = new GameObject[boardWidth, boardHeight];
    private GameObject[] Ghosts;
    private GameObject[] All_Tiles;

    private int GameScore = 100000;
    private int pelletsRemain = 0;


    private GameObject ThePacman;

    public int PacmanLives = 2;


    public AudioClip backgroundNormal;
    public AudioClip backGroundFrighten;

    // Start is called before the first frame update
    void Start()
    {
        
        All_Tiles= GameObject.FindGameObjectsWithTag("Tile");
        Ghosts = GameObject.FindGameObjectsWithTag("Ghost");

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

        UpdateScore();

        GameStatus();

        LivesDisplayed();
    }

    void MonitorGameboard()
    {
        GameObject tile = CheckTile();

        if (tile != null)
        {

            Tiles Tilecomponent = tile.GetComponent<Tiles>();//from the tile gameobject, retrieve the tile script that is attached to it 


            if (Tilecomponent != null )
            {

                if ((Tilecomponent.Is_Pellet || Tilecomponent.Is_Energizer) && !Tilecomponent.getEaten())
                {
                    
                    if(Tilecomponent.Is_Energizer )
                    {
                        foreach(GameObject ScaredGhost in Ghosts)
                        {
                            ScaredGhost.GetComponent<Ghost>().FrightenModeActivate();
                        }   
                    }
                    tile.GetComponent<SpriteRenderer>().enabled = false;
                    Tilecomponent.setEaten(true);
                    pelletsRemain += 1;

                }
 
            }
        }
    }


    void UpdateScore()
    {
        Timepassed += Time.deltaTime;

        if(Timepassed >= 1)
        {
            Timepassed = 0f;
            GameScore -= 1;
            Debug.Log(GameScore);
        }

        HighScore.text = GameScore.ToString();
        
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
        if(pelletsRemain == 244 || PacmanLives == 0)
        {
            SceneManager.LoadScene("MainMenu");
        }

    }

    public void Respawn()
    {
        PacmanLives -= 1;
        GameScore -= 5000;
        ThePacman.GetComponent<PacMan>().Respawn();
        foreach (GameObject ghost in Ghosts)
        {

            ghost.GetComponent<Ghost>().RespawnGhost();

        }
    }

    void LivesDisplayed()
    {
        for(int i = 0; i < PacmanLives - 1; i++)
        {
            LivesRemaining[i].enabled = false;
        }
        for (int i = 0; i < PacmanLives - 1; i++)
        {
            LivesRemaining[i].enabled = true;
        }
    }

}
