using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;


public class WatchAdButton : MonoBehaviour
{

    public GameObject adPanel;
    public MyAdManager adManager;
    public GameObject errorPanel;
    public Text errorText;
    public PlayerStatsManager player;




    // Start is called before the first frame update
    void Start()
    {
        if(PersistentData.instance.isBannerAd())
        {
            adPanel.SetActive(true);
        }
        else
        {
            adPanel.SetActive(false);
        }
        errorPanel.SetActive(false);
    }

    public void onClickWatchAd()
    {
        adManager.ShowRewardedAd();
        /*if(Application.internetReachability != NetworkReachability.NotReachable)
        {
            adManager.ShowRewardedAd();
        }
        else
        {
            errorPanel.SetActive(true);
            PersistentData.instance.pauseGame();
        }*/

    }

    private void AdManager_OnAdDone(object sender, OnAdDoneEventArgs e)
    {
        if (e.PlacementID == MyAdManager.SampleRewarded)
        {
            switch (e.AdShowResult)
            {
                case ShowResult.Failed:
                    Debug.Log("Ad failed play");
                    showErrorText("Ad failed play");
                    break;
                case ShowResult.Skipped: 
                    Debug.Log("Ad was skipped");
                    showErrorText("Ad was skipped");
                    break;
                case ShowResult.Finished:
                    //add points here
                    Debug.Log("Ad finished");
                    player.addReward();
                    showErrorText("Ad finished");
                    break;
            }
        }
    }

    public void onclickOk()
    {
        errorPanel.SetActive(false);
        PersistentData.instance.unpauseGame();
    }

    private void showErrorText(string message)
    {
        errorText.text = message;
        errorPanel.SetActive(true);
        PersistentData.instance.pauseGame();
    }

    public void toggleAd()
    {
        if (PersistentData.instance.isBannerAd())
        {
            adPanel.SetActive(true);
        }
        else
        {
            adPanel.SetActive(false);
        }
    }
}
