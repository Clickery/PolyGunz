using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TouchSample : MonoBehaviour
{
    private void Update()
    {
        int touchCount = Input.touchCount;

        if (touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            Debug.Log($"Got Touch {t.phase}");
        }
    }
}
