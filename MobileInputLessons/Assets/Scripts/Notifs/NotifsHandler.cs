using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Notifications.Android;

public class NotifsHandler : MonoBehaviour
{
    public GameObject prevWaveCleared;
    public Text preScoreUI;

    private void Awake()
    {
        BuildDefaultNoticChannel();
    }

    private void Start()
    {
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
        ProcessData();
    }

    public void ProcessData()
    {
        var data = AndroidNotificationCenter.GetLastNotificationIntent();
        if (data == null)
        {
            prevWaveCleared.SetActive(false);
        }
        else
        {
            prevWaveCleared.SetActive(true);
            string dataString = data.Notification.IntentData;
            preScoreUI.text = "Previous Best: " + dataString;
           
        }
    }

    public void SendDataNotif()
    {
        
        string title = "Debug Notification";
        string text = "Your Score: " + PersistentData.instance.getScore() + ", Waves Cleared: " + PersistentData.instance.getClearedWaves();
        DateTime fireTime = DateTime.Now.AddSeconds(10);

        var notif = new AndroidNotification(title, text, fireTime);
        notif.IntentData = PersistentData.instance.getClearedWaves().ToString();

        AndroidNotificationCenter.SendNotification(notif, "default");
    }


    public void BuildDefaultNoticChannel()
    {
        string channel_id = "default";
        string channel_name = "Default Channel";
        Importance importance = Importance.Default;
        string channel_description = "App default channel";

        var channel = new AndroidNotificationChannel(channel_id, channel_name, channel_description, importance);

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }
}
