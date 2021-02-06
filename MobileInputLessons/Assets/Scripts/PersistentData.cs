using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentData : MonoBehaviour
{
    public static PersistentData instance = null;

    private int playerScore;
    private int waveCount;
    private bool gamePauseState = false;
    private bool showBannerAd = true;
    private PlayerStatsManager player;
    private WaveSpawner spawner;

    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        player = GameObject.FindObjectOfType<PlayerStatsManager>();
        spawner = GameObject.FindObjectOfType<WaveSpawner>();
        //spawner = GameObject
    }

    public void pauseGame()
    {
        gamePauseState = true;
    }

    public bool isGamePaused()
    {
        return gamePauseState;
    }

    public void unpauseGame()
    {
        gamePauseState = false;
    }


    public int getScore()
    {
        return playerScore;
    }

    public int getClearedWaves()
    {
        return waveCount;
    }

    public bool isBannerAd()
    {
        return showBannerAd;
    }

    public void toggleBannderAd()
    {
        showBannerAd = !showBannerAd;
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            playerScore = player.getCurrentScore();
            waveCount = spawner.getCurrentWave();
            //Debug.Log(playerScore + ", " + waveCount);
        }
    }
}
