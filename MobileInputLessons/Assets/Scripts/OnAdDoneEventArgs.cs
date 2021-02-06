using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Advertisements;
public class OnAdDoneEventArgs : EventArgs
{
    public string PlacementID
    {
        get; private set;
    }

    public ShowResult AdShowResult
    {
        get; private set;
    }

    public OnAdDoneEventArgs(string id, ShowResult res)
    {
        PlacementID = id;
        AdShowResult = res;
    }
}
