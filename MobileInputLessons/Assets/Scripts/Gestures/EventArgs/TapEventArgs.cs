using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TapEventArgs : EventArgs
{
    private Vector2 tapPosition;
    private GameObject tappedObject;


    public TapEventArgs(Vector2 pos, GameObject obj = null)
    {
        tapPosition = pos;
        tappedObject = obj;
    }
    
    public Vector2 TapPosition
    {
        get
        {
            return tapPosition;
        }
    }

    public GameObject TappedObject
    {
        get
        {
            return tappedObject;
        }
    }
}
