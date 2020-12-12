using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject gameMenu;
    public GameObject gameControlMenu;
    public GameObject optionMenu;
    public GameObject stageMenu;


    public void StartGame()
    {
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
        gameControlMenu.SetActive(true);
        optionMenu.SetActive(false);
        stageMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void ShowOptionMenu()
    {
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
        gameControlMenu.SetActive(false);
        optionMenu.SetActive(true);
        stageMenu.SetActive(false);
    }

    public void ShowStageMenu()
    {
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
        gameControlMenu.SetActive(false);
        optionMenu.SetActive(false);
        stageMenu.SetActive(true);
    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        gameMenu.SetActive(false);
        gameControlMenu.SetActive(false);
        optionMenu.SetActive(false);
        stageMenu.SetActive(false);
    }
}
