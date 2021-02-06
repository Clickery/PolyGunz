using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

using System;

public class FBManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject fbPostPanel;
    public GameObject postButton;
    public GameObject uploadingPanel;
    public GameObject uploadDonePanel;

    public Text scoreText;
    public Text waveClearedText;

    private void Awake()
    {
        if (postButton != null && uploadingPanel != null && uploadDonePanel != null && gameOverPanel != null && fbPostPanel != null)
        {
            postButton.SetActive(false);
            uploadingPanel.SetActive(false);
            uploadDonePanel.SetActive(false);
            gameOverPanel.SetActive(true);
            fbPostPanel.SetActive(false);
        }

        if (scoreText != null && waveClearedText != null)
        {
            scoreText.text = "Score: " + PersistentData.instance.getScore().ToString();
            waveClearedText.text = "Waves Cleared: " + PersistentData.instance.getClearedWaves().ToString();
        }

        if (fbPostPanel != null)
        {
            fbPostPanel.SetActive(false);
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (!FB.IsInitialized)
        {
            FB.Init(OnInitDone, OnFBHide);
        }

        else
        {
            FB.ActivateApp();
        }
    }

    public void OnInitDone()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            Debug.Log("FB Initialize");
        }

        else
        {
            Debug.LogError("FB Not Initialize");
        }
    }

    public void OnFBHide(bool shown)
    {
        if (shown)
        {
            Time.timeScale = 1;
        }

        else
        {
            Time.timeScale = 0;
        }
    }

    public void LoginFB()
    {
        if (FB.IsInitialized)
        {

            if (!FB.IsLoggedIn)
            {
                List<string> permissions = new List<string>() { "public_profile", "email" };
                FB.LogInWithReadPermissions(permissions, OnFBLoginDone);

                if (postButton != null && uploadingPanel != null && uploadDonePanel != null && gameOverPanel != null && fbPostPanel != null)
                {
                    postButton.SetActive(true);
                    uploadingPanel.SetActive(false);
                    uploadDonePanel.SetActive(false);
                    gameOverPanel.SetActive(true);
                    fbPostPanel.SetActive(false);
                }
            }

            else
            {
                UploadPhoto();
                Debug.Log("User already logged in");
            }
        }
        else
        {
            Debug.Log("FB not yet initialize");
        }
    }

    public void OnFBLoginDone(ILoginResult res)
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("Logged IN");
        }

        else
        {
            Debug.LogError("Error Logging In: " + res.Error);
        }
    }

    public void UploadPhoto()
    {
        StartCoroutine(UploadPhotoRoutine());
    }

    IEnumerator UploadPhotoRoutine()
    {
        if (postButton != null && uploadingPanel != null && uploadDonePanel != null && gameOverPanel != null && fbPostPanel != null)
        {
            postButton.SetActive(false);
            uploadingPanel.SetActive(false);
            uploadDonePanel.SetActive(false);
            gameOverPanel.SetActive(false);
            fbPostPanel.SetActive(true);
        }

        yield return new WaitForEndOfFrame();


        if (fbPostPanel != null)
        {
            fbPostPanel.SetActive(false);
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Texture2D screen = ScreenCapture.CaptureScreenshotAsTexture();
        byte[] screen_byte = screen.EncodeToPNG();

        WWWForm form = new WWWForm();
        form.AddBinaryData("image", screen_byte, "screen.png");
        form.AddField("caption", "Score: " + PersistentData.instance.getScore().ToString() + "\n Wave Cleared: " +PersistentData.instance.getClearedWaves().ToString());
        string fb_url = "me/photos";

        FB.API(fb_url, HttpMethod.POST, OnUploadDone, form);
        Debug.Log("Uploading");

        if (postButton != null && uploadingPanel != null && uploadDonePanel != null && gameOverPanel != null && fbPostPanel != null)
        {
            postButton.SetActive(false);
            uploadingPanel.SetActive(true);
            uploadDonePanel.SetActive(false);
            gameOverPanel.SetActive(false);
            fbPostPanel.SetActive(false);
        }
    }

    public void OnUploadDone(IGraphResult res)
    {
        if (string.IsNullOrEmpty(res.Error))
        {
            Debug.Log("Upload Done");
            if (postButton != null && uploadingPanel != null && uploadDonePanel != null && gameOverPanel != null && fbPostPanel != null)
            {
                postButton.SetActive(false);
                uploadingPanel.SetActive(false);
                uploadDonePanel.SetActive(true);
                gameOverPanel.SetActive(false);
                fbPostPanel.SetActive(false);
            }
        }

        else
        {
            Debug.LogError("Error: " + res.Error);
        }
    }

    public void PressedOkUploadDone()
    {
        if (postButton != null && uploadingPanel != null && uploadDonePanel != null && gameOverPanel != null && fbPostPanel != null)
        {
            postButton.SetActive(true);
            uploadingPanel.SetActive(false);
            uploadDonePanel.SetActive(false);
            gameOverPanel.SetActive(true);
            fbPostPanel.SetActive(false);
        }
    }

}
