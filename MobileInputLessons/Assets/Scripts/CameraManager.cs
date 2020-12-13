using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera portraitCamera;
    public Camera landscapeCamera;

    // Update is called once per frame
    void Update()
    {
        portraitCamera.enabled = Screen.width <= Screen.height;
        portraitCamera.GetComponent<AudioListener>().enabled = portraitCamera.enabled;
        landscapeCamera.enabled = Screen.width > Screen.height;
        landscapeCamera.GetComponent<AudioListener>().enabled = landscapeCamera.enabled;
    }
}
