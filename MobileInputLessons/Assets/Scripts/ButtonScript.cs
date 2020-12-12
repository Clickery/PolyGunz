using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject parentOptionsPanel;
    public GameObject topLeftBackButtonPanel;
    public GameObject parentPanel;
    public GameObject optionPanel;
    public GameObject levelSelectionPanel;

    private void Start()
    {
        mainMenuPanel.SetActive(true);
        parentOptionsPanel.SetActive(false);
        topLeftBackButtonPanel.SetActive(false);
        parentPanel.SetActive(false);
        optionPanel.SetActive(false);
        levelSelectionPanel.SetActive(false);
    }

    public void StartGame()
    {
        Debug.Log("Start Level 1!");
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        Debug.Log("Back MainMenu From Level");
        SceneManager.LoadScene(0);
    }

    public void BackToMainMenuPanel()
    {
        mainMenuPanel.SetActive(true);
        parentOptionsPanel.SetActive(false);
        topLeftBackButtonPanel.SetActive(false);
        parentPanel.SetActive(false);
        optionPanel.SetActive(false);
        levelSelectionPanel.SetActive(false);
    }

    public void ShowOptionMenu()
    {
        mainMenuPanel.SetActive(false);
        parentOptionsPanel.SetActive(true);
        topLeftBackButtonPanel.SetActive(true);
        parentPanel.SetActive(true);
        optionPanel.SetActive(true);
        levelSelectionPanel.SetActive(false);
    }

    public void ShowStageLevels()
    {
        mainMenuPanel.SetActive(false);
        parentOptionsPanel.SetActive(true);
        topLeftBackButtonPanel.SetActive(true);
        parentPanel.SetActive(true);
        optionPanel.SetActive(false);
        levelSelectionPanel.SetActive(true);
    }
}