using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnClick : MonoBehaviour
{
    public GameObject cameraGameObject;
    private float rotateQuarterHalf = 90.0f;

    public void rotateToTheLeft()
    {
        cameraGameObject.transform.Rotate(0.0f, (-1.0f * rotateQuarterHalf), 0.0f);
    }

    public void rotateToTheRight()
    {
        cameraGameObject.transform.Rotate(0.0f, rotateQuarterHalf, 0.0f);
    }
}
