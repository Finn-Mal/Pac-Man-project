using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimations : MonoBehaviour
{
    private Ghost GhostInformation;
    private Vector2 direction;
    private enum Mode { Chase, Scatter, Frighten }
    private Mode currentMode;


    private float frightenModeTimer = 0;


    public RuntimeAnimatorController Up;
    public RuntimeAnimatorController Right;
    public RuntimeAnimatorController Left;
    public RuntimeAnimatorController Down;
    public RuntimeAnimatorController frightenGhost;
    public RuntimeAnimatorController endingFrighten;

    // Start is called before the first frame update
    void Start()
    {
        GhostInformation = transform.GetComponent<Ghost>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    /*
    public void UpdateAnimation()
    {
        if (currentMode != Mode.Frighten)
        {
            if (Direction == Vector2.left)
            {
                transform.GetComponent<Animator>().runtimeAnimatorController = ghostLeft;
            }
            if (Direction == Vector2.right)
            {
                transform.GetComponent<Animator>().runtimeAnimatorController = ghostRight;
            }
            if (Direction == Vector2.up)
            {
                transform.GetComponent<Animator>().runtimeAnimatorController = ghostUp;
            }
            if (Direction == Vector2.down)
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
    }*/


}
