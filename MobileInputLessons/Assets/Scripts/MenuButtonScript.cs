using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class MenuButtonScript : MonoBehaviour
{
    public GameObject menuPanel;
    public MyAdManager adManager;
    void Start()
    {
        menuPanel.SetActive(false);
    }

    public void onClickBackToMenuButtom()
    {
        SceneManager.LoadScene(0);
        PersistentData.instance.unpauseGame();
    }

    public void onClickMenuButton()
    {
        menuPanel.SetActive(true);
        PersistentData.instance.pauseGame();
    }

    public void onClickBackButton()
    {
        menuPanel.SetActive(false);
        PersistentData.instance.unpauseGame();
    }
}
