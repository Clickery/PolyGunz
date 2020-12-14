using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSample : MonoBehaviour
{
    public Sprite Began;
    public Sprite Stationary;
    public Sprite Moved;
    public Sprite Ended;

    private SpriteRenderer sR;

    private void OnEnable()
    {
        sR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        int touchCount = Input.touchCount;
        if (touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            
            switch (t.phase)
            {
                case TouchPhase.Began: sR.sprite = Began; break;
                case TouchPhase.Stationary: sR.sprite = Stationary; break;
                case TouchPhase.Moved: sR.sprite = Moved; break;
                //case TouchPhase.Canceled: break;
                case TouchPhase.Ended: sR.sprite = Ended; break;
            }
        }

    }

    private void OnDrawGizmos()
    {
        int touchCount = Input.touchCount;
        Debug.Log("OnDrawGizmos");
        if (touchCount > 0)
        {
            for(int i = 0; i < touchCount; i++)
            {
                Touch t = Input.GetTouch(0);
                Vector2 t_pos = t.position;

                Ray r = Camera.main.ScreenPointToRay(t_pos);
                switch(t.fingerId)
                {
                    //change sprite accordingly
                    case 0: Gizmos.DrawIcon(r.GetPoint(4), "MonikaChibi"); break;
                    case 1: Gizmos.DrawIcon(r.GetPoint(4), "MonikaChibi"); break;
                    case 2: Gizmos.DrawIcon(r.GetPoint(4), "MonikaChibi"); break;
                    case 3: Gizmos.DrawIcon(r.GetPoint(4), "MonikaChibi"); break;
                    case 4: Gizmos.DrawIcon(r.GetPoint(4), "MonikaChibi"); break;
                }
                
            }
        }
    }
}
