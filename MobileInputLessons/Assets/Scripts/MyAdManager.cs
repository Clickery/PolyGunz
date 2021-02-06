using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;

public class MyAdManager : MonoBehaviour, IUnityAdsListener
{
    public event EventHandler<OnAdDoneEventArgs> OnAdDone;

    public string GameID
    {
        get
        {
#if UNITY_ANDROID
            return "3996677";
#elif UNITY_IOS
            return "3996676";
#endif
        }
    }

    public const string SampleBanner = "sampleBanner";
    public const string SampleRewarded = "rewardedVideo";

    //public GameObject RewardAdButton;


    public bool Test = true;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Advertisement.AddListener(this);
        Advertisement.Initialize(GameID, Test);

        if (PersistentData.instance.isBannerAd() && Application.internetReachability != NetworkReachability.NotReachable)
        {
            BannerAd();
        }
        else
        {
            HideBannerAd();
        }
    }

    public void BannerAd()
    {
        StartCoroutine(ShowBannerAdRoutine());
    }

    public void HideBannerAd()
    {
        if (Advertisement.Banner.isLoaded)
        {
            Advertisement.Banner.Hide();
        }
    }
    IEnumerator ShowBannerAdRoutine()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Banner.Show(SampleBanner);
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log($"{placementId} is done loading");

    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError($"Error loading ads{message}");
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log($"{placementId} is playing");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
       
        if (OnAdDone != null)
        {
            Debug.Log("isa kang BONAK!");
            OnAdDoneEventArgs args = new OnAdDoneEventArgs(placementId, showResult);
            OnAdDone(this, args);
        }
    }

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady(SampleRewarded))
        {
            Advertisement.Show(SampleRewarded);
        }
        else
        {
            Debug.Log("No rewarded ads");
        }
    }


    public void toggleBannerAds()
    {
        PersistentData.instance.toggleBannderAd();

        if(PersistentData.instance.isBannerAd() && Application.internetReachability != NetworkReachability.NotReachable)
        {
            BannerAd();
        }
        else
        {
            HideBannerAd();
        }
    }
}
