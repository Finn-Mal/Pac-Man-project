using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    public float movespeed = 3.9f; //move speed of Ghost to pacman (always slightly slow in first couple levels
    private float FrightenSpeed = 3.0f;

    private float ghostSpeed;

    public Node StartNode; //first node for Ghost to travel too
    public Node ScatterNode;

    public float GhostReleaseTimer = 0f;
    public bool inGhostHouse = false;
    public float ThisGhostTimer;

    public bool ghostIsEaten = false;

    public int scatterModePhase = 7;
    public int chaseModePhase = 20;
    public int frightenModeDuration = 10;
    public int endingFrightenMode = 7;



    public int scatterModePhaseHard = 5;
    public int currentscatterModePhase;

    private bool blinkingFrightenWhite = false;

    private int stateIteration = 1;
    private float stateTimer = 0;
    private float frightenModeTimer = 0;

    public enum Mode { Chase, Scatter, Frighten } // ghost states of choice
    public enum GhostType { Red, Pink, Blue, Orange } //Blinky, Pinky, Inky, Clyde

    Mode currentMode = Mode.Scatter;
    Mode previousMode;
    public GhostType theGhost;

    private GameObject Pacman;


    private Node currentNode, nextNode, previousNode;
    private Vector2 currectDirection, nextDirection;


    public GameObject BlinkyTheGhost;

    public RuntimeAnimatorController ghostUp;
    public RuntimeAnimatorController ghostRight;
    public RuntimeAnimatorController ghostLeft;
    public RuntimeAnimatorController ghostDown;
    public RuntimeAnimatorController frightenGhost;
    public RuntimeAnimatorController endingFrighten;





    // Start is called before the first frame update
    void Start()
    {

        ghostSpeed = movespeed;
        Pacman = GameObject.FindGameObjectWithTag("Player");

        Node initialNode = StartNode;

        if (initialNode != null)
        {
            currentNode = initialNode;
        }
        previousNode = currentNode;
        currentscatterModePhase = scatterModePhase;

        if (inGhostHouse)
        {
            currectDirection = Vector2.up;
            nextNode = currentNode.Neighbours[0];
        }
        else if (!inGhostHouse)
        {
            currectDirection = Vector2.left;
            nextNode = SelecteNextNode();
        }

    }

    // Update is called once per frame
    void Update()
    {
        modeUpdate();

        moveGhost();

        ReleaseGhost();

        UpdateAnimation();

    }

    void UpdateAnimation()
    {
        if (currentMode != Mode.Frighten)
        {
            if (currectDirection == Vector2.left)
            {
                transform.GetComponent<Animator>().runtimeAnimatorController = ghostLeft;
            }
            if (currectDirection == Vector2.right)
            {
                transform.GetComponent<Animator>().runtimeAnimatorController = ghostRight;
            }
            if (currectDirection == Vector2.up)
            {
                transform.GetComponent<Animator>().runtimeAnimatorController = ghostUp;
            }
            if (currectDirection == Vector2.down)
            {
                transform.GetComponent<Animator>().runtimeAnimatorController = ghostDown;
            }
        }
        else if (currentMode == Mode.Frighten)
        {
            if (frightenModeTimer < endingFrightenMode)
            {
                transform.GetComponent<Animator>().runtimeAnimatorController = frightenGhost;
            }
            else if (frightenModeTimer >= endingFrightenMode)
            {
                transform.GetComponent<Animator>().runtimeAnimatorController = endingFrighten;
            }

        }
    }

    void moveGhost()
    {
        if (nextNode != currentNode && nextNode != null && !inGhostHouse) // if next ndoe ghost trevels exists and isn't current target
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

            }
            else
            {
                transform.localPosition += (Vector3)currectDirection * ghostSpeed * Time.deltaTime;
            }
        }

    }


    void modeUpdate()
    {
        if (currentMode != Mode.Frighten)
        {
            stateTimer += Time.deltaTime;
            if (stateIteration < 4)
            {
                if (currentMode == Mode.Scatter && stateTimer > currentscatterModePhase)
                {
                    changeMode(Mode.Chase);
                    stateTimer = 0f;
                }
                if (currentMode == Mode.Chase && stateTimer > chaseModePhase)
                {

                    changeMode(Mode.Scatter);
                    stateTimer = 0f;
                }
            }
            else
            {
                stateIteration++;
                changeMode(Mode.Chase);
            }

            if (stateIteration > 2)
            {
                currentscatterModePhase = scatterModePhaseHard;
            }

        }

        else
        {
            ghostSpeed = FrightenSpeed;
            frightenModeTimer += Time.deltaTime;
            if (frightenModeTimer >= frightenModeDuration)
            {
                frightenModeTimer = 0;
                currentMode = previousMode;
            }
        }
    }

    void changeMode(Mode newMode)
    {
        currentMode = newMode;
    }



    //module for Gameboard to call to update ghost mode to frighten
    public void FrightenModeActivate()
    {
        frightenModeTimer = 0;


        if (currentMode != Mode.Frighten)
        {

            if (!inGhostHouse)
            {
                Node temp = previousNode;
                previousNode = nextNode;
                nextNode = temp;
                currectDirection *= -1;
                previousMode = currentMode;
            }

        }
        currentMode = Mode.Frighten;
    }

    /* Node FrightenModeLogic()
     {
         Node moveToNode = null;
         if (transform.transform.position == nextNode.transform.position)
         {
             int i = nextNode.GetComponent<Node>().Neighbours.Length;
             int rngindex = (int)Random.Range(0f, i - 1);
             currectDirection = nextNode.GetComponent<Node>().valid_Direction[rngindex];
             moveToNode = nextNode.GetComponent<Node>().Neighbours[rngindex];

         }

         return moveToNode;
     }*/




    Vector2 BlinkyTile()
    {
        Vector2 targetTile = Vector2.zero;

        targetTile = new Vector2(Mathf.RoundToInt(Pacman.transform.position.x), Mathf.RoundToInt(Pacman.transform.position.y));


        return targetTile;
    }

    Vector2 PinkyTile()
    {

        Vector2 targetTile = Vector2.zero;

        //must know pacman's orientation and position as Pinky always tries to be 4 tiles infront of pacman
        Vector2 PacPosition = Pacman.transform.localPosition;
        int PacX = Mathf.RoundToInt(PacPosition.x);
        int PacY = Mathf.RoundToInt(PacPosition.y);

        Vector2 PacmanOrientation = Pacman.GetComponent<PacMan>().Orientation; //gives the direction pacman is currently heading i.e Vector.up

        Vector2 PacTile = new Vector2(PacX, PacY);
        targetTile = PacTile + (4 * PacmanOrientation);//4 * Orientation will give exactly 4 units head of where Pacman will be


        return targetTile;

    }

    Vector2 InkyTile()
    {
        Vector2 targetTile = Vector2.zero;
        //must know pacman's orientation and position as Pinky always tries to be 4 tiles infront of pacman
        Vector2 PacPosition = Pacman.transform.localPosition;
        int PacX = Mathf.RoundToInt(PacPosition.x);
        int PacY = Mathf.RoundToInt(PacPosition.y);

        Vector2 PacmanOrientation = Pacman.GetComponent<PacMan>().Orientation; //gives the direction pacman is currently heading i.e Vector.up

        Vector2 PacTile = new Vector2(PacX, PacY);
        Vector2 FirstTileCal = PacTile + (2 * PacmanOrientation);//4 * Orientation will give exactly 4 units head of where Pacman will be



        int BlinkTargetx = (int)BlinkyTheGhost.transform.position.x;
        int BlinkTargety = (int)BlinkyTheGhost.transform.position.y;

        Vector2 TempBlink = new Vector2(BlinkTargetx, BlinkTargety);

        float distance = MeasureDistance(FirstTileCal, TempBlink) * 2;

        targetTile = new Vector2(TempBlink.x + distance, TempBlink.y + distance);

        return targetTile;
    }

    Vector2 Clydetile()
    {
        Vector2 targetTile = Vector2.zero;
        float distance = MeasureDistance(transform.localPosition, Pacman.transform.localPosition);
        if (distance > 8) //distance is less then 8 tiles from pacman
        {
            //acts like he is in scatter mode
            targetTile = new Vector2(Mathf.RoundToInt(Pacman.transform.position.x), Mathf.RoundToInt(Pacman.transform.position.y));


        }
        else if (distance <= 8) //distance is greater then 8 tiles from pacman
        {
            targetTile = ScatterNode.transform.position;
        }


        return targetTile;
    }



    Vector2 ChooseTile()
    {
        Vector2 targetTile = Vector2.zero; //Vector that will define the tile X Y position Pacman is in
        if (theGhost == GhostType.Red)
        {
            targetTile = BlinkyTile();
        }
        else if (theGhost == GhostType.Pink)
        {
            targetTile = PinkyTile();
        }
        else if (theGhost == GhostType.Blue)
        {
            targetTile = InkyTile();
        }
        else if (theGhost == GhostType.Orange)
        {
            targetTile = Clydetile();
        }


        return targetTile;
    }


    void ReleaseGhost()
    {

        GhostReleaseTimer += Time.deltaTime;
        if (GhostReleaseTimer >= ThisGhostTimer)
        {
            inGhostHouse = false;
        }


    }

    Node SelecteNextNode()
    {
        Node moveToNode = null;

        Vector2 targetTile = Vector2.zero; //Vector that will define the tile X Y position Pacman is in
                                           //
        if (currentMode == Mode.Chase)
        {
            targetTile = ChooseTile();
        }
        else if (currentMode == Mode.Scatter)
        {
            targetTile = ScatterNode.transform.position;
        }



        Node[] foundNodes = new Node[4];
        Vector2[] foundNodesDirection = new Vector2[4]; //from current position, array of one of four nodes Ghost can move too

        int nodeCounter = 0;

        for (int i = 0; i < currentNode.Neighbours.Length; i++)
        {
            if (currentNode.valid_Direction[i] != currectDirection * -1) // avoid ghost going in reverse
            {
                foundNodes[nodeCounter] = currentNode.Neighbours[i];
                foundNodesDirection[nodeCounter] = currentNode.valid_Direction[i];
                nodeCounter++;
            }
        }


        float leastDistance = 9999f;

        for (int i = 0; i < foundNodes.Length; i++)
        {

            if (foundNodesDirection[i] != Vector2.zero)
            {

                float distance = MeasureDistance(foundNodes[i].transform.position, targetTile); // measures the distance between 

                if (distance < leastDistance)
                {
                    leastDistance = distance;
                    moveToNode = foundNodes[i];
                    currectDirection = foundNodesDirection[i];
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
            if (tile.GetComponent<Node>() != null)
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

    float MeasureDistance(Vector2 posA, Vector2 posB) // Euclindean distance
    {

        float dx = posA.x - posB.x;
        float dy = posA.y - posB.y;

        float distance = Mathf.Sqrt(dx * dx + dy * dy); //pythagoras used to measure distance between ghost and Pacman

        return distance;
    }



    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("GameObject1 collided with " + col.name);

    }

    public Vector2 getDirection()
    {
        return currectDirection;
    }

    public Mode getMode()
    {
        return currentMode;
    }

    public float getFrightenTimer()
    {
        return frightenModeTimer;
    }

}
