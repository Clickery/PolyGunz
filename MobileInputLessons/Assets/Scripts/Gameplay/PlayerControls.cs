using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Joystick joystick;
    private float sensitivity = 1000.0f;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(joystick.Horizontal * sensitivity * Time.deltaTime, joystick.Vertical * sensitivity * Time.deltaTime, 0);
    }
}
