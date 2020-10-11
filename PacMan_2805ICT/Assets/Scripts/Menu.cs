using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private enum MenuChoice {PlayGame, Options, Exit } 
    private MenuChoice ToLoad;

    public Text PlayGame;
    public Text Options;
    public Text Quit;

    public Image Selector;

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)) && Selector.transform.position.y != PlayGame.transform.position.y)
        {
            Selector.transform.localPosition = new Vector3(Selector.transform.localPosition.x, Selector.transform.localPosition.y + 75f, Selector.transform.localPosition.z);
        }
        else if ((Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)) && Selector.transform.position.y != Quit.transform.position.y)
        {
            Selector.transform.localPosition = new Vector3(Selector.transform.localPosition.x, Selector.transform.localPosition.y  - 75f, Selector.transform.localPosition.z);
        }


        if(Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return))
        {
            LoadMenuOption();
        }

        checkMenuposition();
    }

    void checkMenuposition()
    {
        if(Selector.transform.position.y == PlayGame.transform.position.y)
        {
            ToLoad = MenuChoice.PlayGame;
        }
        else if(Selector.transform.position.y == Options.transform.position.y)
        {
            ToLoad = MenuChoice.Options;
        }
        else if(Selector.transform.position.y == Options.transform.position.y)
        {
            ToLoad = MenuChoice.Exit;
        }
    }

    void LoadMenuOption()
    {

        if(ToLoad == MenuChoice.PlayGame)
        {
            SceneManager.LoadScene("Level1");
        }
        else if(ToLoad == MenuChoice.Options)
        {
            //SceneManage.LoadScene("Options");
        }
        else if(ToLoad == MenuChoice.Exit)
        {
            Application.Quit();
        }


    }
}
