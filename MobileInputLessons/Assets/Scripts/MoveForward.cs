﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private float movementSpeedInZ = 1.0f;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0,0, -1.0f*(movementSpeedInZ*Time.deltaTime));
    }
}
