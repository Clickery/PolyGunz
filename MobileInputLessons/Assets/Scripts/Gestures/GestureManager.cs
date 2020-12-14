using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GestureManager : MonoBehaviour
{
    public static GestureManager Instance;

    public TwoFingerPanProperty _twoFingerPan;

    public TapProperty _tapProperty;
    public SwipeProperty _swipeProperty;
    public DragProperty _dragProperty;

    public event EventHandler<TapEventArgs> OnTap;
    public event EventHandler<SwipeEventArgs> OnSwipe;
    public event EventHandler<DragEventArgs> OnDrag;

    private Vector2 start_pos;
    private Vector2 end_pos;
    private float gesture_time;

    private Touch gesture_finger1;
    private Touch gesture_finger2;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 1)
        {
            CheckSingleFingerGesture();
        }
        else if(Input.touchCount > 1)
        {
            gesture_finger1 = Input.GetTouch(0);
            gesture_finger2 = Input.GetTouch(1);
        }

    }

    //drag is getting cut off at a certain distance for some reason
    private void FireDragEvent()
    {
        Debug.Log($"Drag {gesture_finger1.position.ToString()}");

        GameObject hitObject = GetHit(gesture_finger1.position);

        DragEventArgs args = new DragEventArgs(gesture_finger1, hitObject);

        if(OnDrag != null)
        {
            
            OnDrag(this, args);
        }

        if(hitObject != null)
        {
            IDragged drag = hitObject.GetComponent<IDragged>();
            if(drag != null)
            {
                drag.OnDrag(args);
            }
        }
    }

    private void FireSwipeEvent()
    {
        Debug.Log("swipe!");
        Vector2 diff = end_pos - start_pos;

        GameObject hitObject = GetHit(start_pos);

        Directions dir;

        //swiping left or right
        if(Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
        {
            if(diff.x > 0)
            {
                Debug.Log("swiped right!");
                dir = Directions.RIGHT;
            }
            else
            {
                Debug.Log("swiped left!");
                dir = Directions.LEFT;
            }
        }
        //swiping up or down
        else
        {
            if (diff.y > 0)
            {
                Debug.Log("swiped upwards!");
                dir = Directions.UP;
            }
            else
            {
                Debug.Log("swiped downwards!");
                dir = Directions.DOWN;
            }
        }

        SwipeEventArgs args = new SwipeEventArgs(start_pos, diff, dir, hitObject);
        if(OnSwipe != null)
        {
            OnSwipe(this, args);
        }
        
        if(hitObject != null)
        {
            ISwiped iswiped = hitObject.GetComponent<ISwiped>();
            if(iswiped != null)
            {
                iswiped.OnSwipe(args);
            }
        }
    }

    private GameObject GetHit(Vector2 screenPos)
    {
        Ray r = Camera.main.ScreenPointToRay(start_pos);
        RaycastHit hit = new RaycastHit();
        GameObject hitObj = null;

        if (Physics.Raycast(r, out hit, Mathf.Infinity))
        {
            hitObj = hit.collider.gameObject;
        }

        return hitObj;
    }

    private void FireTapEvent()
    {
        Debug.Log("Tap!, " + gesture_time);
        if (OnTap != null)
        {
            Ray r = Camera.main.ScreenPointToRay(start_pos);
            RaycastHit hit = new RaycastHit();
            GameObject hitObj = null;
            
            if(Physics.Raycast(r, out hit, Mathf.Infinity))
            {
                hitObj = hit.collider.gameObject;
            }

            TapEventArgs args = new TapEventArgs(start_pos, hitObj);
            OnTap(this, args);

            if(hitObj != null)
            {
                ITapped tapped = hitObj.GetComponent<ITapped>();
                if(tapped != null)
                {
                    tapped.OnTap();
                }
            }
        }
    }


    private void OnDrawGizmos()
    {
        if(Input.touchCount > 0)
        {
            Ray r = Camera.main.ScreenPointToRay(gesture_finger1.position);
            Gizmos.DrawIcon(r.GetPoint(5), "NatsukiChibi");
            if (Input.touchCount > 1)
            {
                r = Camera.main.ScreenPointToRay(gesture_finger1.position);
                Gizmos.DrawIcon(r.GetPoint(5), "MonikaChibi");
            }
        }
    }

    private void CheckSingleFingerGesture()
    {
        gesture_finger1 = Input.GetTouch(0);
        if (gesture_finger1.phase == TouchPhase.Began)
        {
            start_pos = gesture_finger1.position;
            gesture_time = 0;
        }

        if (gesture_finger1.phase == TouchPhase.Ended)
        {
            end_pos = gesture_finger1.position;

            //commented code below is for swiping
            if (gesture_time <= _swipeProperty.MaxGestureTime &&
                Vector2.Distance(start_pos, end_pos) >= (_swipeProperty.MinSwipeDistance * Screen.dpi))
            {
                FireSwipeEvent();
            }

            //commented code below is for tapping
            if (gesture_time <= _tapProperty.tap_time &&
                Vector2.Distance(start_pos, end_pos) <= (_tapProperty.tap_distance * Screen.dpi))
            {
                FireTapEvent();
            }
        }
        else
        {
            gesture_time += Time.deltaTime;
            if (gesture_time >= _dragProperty.MinGesture)
            {
                FireDragEvent();
            }
        }
    }
}
