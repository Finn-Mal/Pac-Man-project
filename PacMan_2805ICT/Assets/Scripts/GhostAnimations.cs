using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimations : MonoBehaviour
{
    private Ghost GhostInformation;
    private Vector2 direction;
    private Ghost.Mode ghostMode;


    private float frightenModeTimer = 0;
    //private float endingFrightenMode = 0;


    public RuntimeAnimatorController Up;
    public RuntimeAnimatorController Right;
    public RuntimeAnimatorController Left;
    public RuntimeAnimatorController Down;
    public RuntimeAnimatorController frightenGhost;
    public RuntimeAnimatorController endingFrighten;
    public RuntimeAnimatorController EyesLeft;
    public RuntimeAnimatorController EyesRight;
    public RuntimeAnimatorController EyesUp;
    public RuntimeAnimatorController EyesDown;

    // Start is called before the first frame update
    void Start()
    {
        GhostInformation = transform.GetComponent<Ghost>();
        direction = GhostInformation.getDirection();
        ghostMode = GhostInformation.getMode();
    }

    // Update is called once per frame
    void Update()
    {
        frightenModeTimer = GhostInformation.getFrightenTimer();
        direction = GhostInformation.getDirection();
        ghostMode = GhostInformation.getMode();
        UpdateAnimation();
    }
    
    public void UpdateAnimation()
    {
        if (ghostMode == Ghost.Mode.Chase || ghostMode == Ghost.Mode.Scatter)
        {

            
            if (direction == Vector2.left)
            {
                transform.GetComponent<Animator>().runtimeAnimatorController = Left;
            }
            if (direction == Vector2.right)
            {
                transform.GetComponent<Animator>().runtimeAnimatorController = Right;
            }
            if (direction == Vector2.up)
            {
                transform.GetComponent<Animator>().runtimeAnimatorController = Up;
            }
            if (direction == Vector2.down)
            {
                transform.GetComponent<Animator>().runtimeAnimatorController = Down;
            }
        }
        else if (ghostMode == Ghost.Mode.Frighten)
        {
            if (frightenModeTimer < GhostInformation.endingFrightenMode)
            {
                transform.GetComponent<Animator>().runtimeAnimatorController = frightenGhost;
            }
            else if (frightenModeTimer >= GhostInformation.endingFrightenMode)
            {
                transform.GetComponent<Animator>().runtimeAnimatorController = endingFrighten;
            }

        }
        else if(ghostMode == Ghost.Mode.Eaten)
        {
            Debug.Log("GHOST HAS BEEN EATEN");
            if (direction == Vector2.left)
            {
                transform.GetComponent<Animator>().runtimeAnimatorController = EyesLeft;
            }
            if (direction == Vector2.right)
            {
                transform.GetComponent<Animator>().runtimeAnimatorController = EyesRight;
            }
            if (direction == Vector2.up)
            {
                transform.GetComponent<Animator>().runtimeAnimatorController = EyesUp;
            }
            if (direction == Vector2.down)
            {
                transform.GetComponent<Animator>().runtimeAnimatorController = EyesDown;
            }
        }


    }


}
