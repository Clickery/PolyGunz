using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapSampleReciever : MonoBehaviour
{
    public GameObject[] spawns;
    private int index = 0;

    public void Start()
    {
        GestureManager.Instance.OnTap += OnTap;
    }

    public void OnDisable()
    {
        GestureManager.Instance.OnTap -= OnTap;
    }


    private void OnTap(object sender, TapEventArgs e)
    {
        if(e.TappedObject == null)
        {
            Debug.Log("you tapped on nothing!");
            Vector2 pos = e.TapPosition;
            Ray r = Camera.main.ScreenPointToRay(pos);

            Spawn(r.GetPoint(10));
        }
        else
        {
            Debug.Log($"Hit {e.TappedObject.name}");
        }
       
    }



    private void Spawn(Vector3 pos)
    {
        Instantiate<GameObject>(spawns[index % 2], pos, Quaternion.identity);
        index++;
    }
}
