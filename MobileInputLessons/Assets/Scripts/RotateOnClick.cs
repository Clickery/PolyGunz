using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnClick : MonoBehaviour
{
    public GameObject cameraGameObject;
    private float rotateQuarterHalf = 90.0f;

    public void rotateToTheLeft()
    {
        cameraGameObject.transform.Rotate(Vector3.up * rotateQuarterHalf);
    }

    public void rotateToTheRight()
    {
        cameraGameObject.transform.Rotate(Vector3.down * rotateQuarterHalf);
    }
}
