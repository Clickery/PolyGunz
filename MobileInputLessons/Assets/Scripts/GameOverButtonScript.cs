using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtonScript : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject playAgainButton;
    public GameObject mainMenuButton;

    void Start()
    {
        gameOverPanel.SetActive(true);
        playAgainButton.SetActive(true);
        mainMenuButton.SetActive(true);
    }

    public void GameOver()
    {
        /*
        if(gameover == true){
            gameOverPanel.SetActive(true);
            playAgainButton.SetActive(true);
            mainMenuButton.SetActive(true);
        } 
        */
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
