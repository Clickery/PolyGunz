using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    
    public Joystick joystick;
    public RectTransform crossHair;
    private float sensitivity = 2.0f;

    //script references///////////////
    public PlayerStatsManager playerStats;
    public GunStatsManager gunStatManager;
    //////////////////////////////////

    private Touch gesture_finger1;
    private Touch gesture_finger2;

    //Touch purposes/////////////////////
    public SwipeProperty _swipeProperty;
    //finger touch
    private Vector2 start_pos;
    private Vector2 end_pos;
    private float gesture_time;
    //Touch purposes/////////////////////

    //Shake purposes////////////////////
    private Vector3 phoneAccel;
    private float reloadTrigger = 4.0f;
    //Shake purposes////////////////////

    //Pinch/Spread purposes////////////
    public PinchSpreadProperty _pinchSpread;

    private int index = 0;

    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        //aiming
        if(joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            aimCrossHair();
        }
       
        if (Input.touchCount == 1) //changing gun type
        {
            gesture_finger1 = Input.GetTouch(0);
            checkForSwipe();
        }
        else if(Input.touchCount > 1 && gunStatManager.hasNuke())// nuking purposes
        {
            gesture_finger1 = Input.GetTouch(0);
            gesture_finger2 = Input.GetTouch(1);
            checheckforPinchSpread();
        }

        //Reload
        checkForShake();
    }

    private void aimCrossHair()
    { 
        
        crossHair.position += new Vector3(joystick.Horizontal * sensitivity * Screen.dpi * Time.deltaTime, 
            joystick.Vertical * sensitivity * Screen.dpi * Time.deltaTime, 0);
       
    }

    private void checkForSwipe()
    {
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
                fireSwipeEvent();
            }
        }
    }

    private void checheckforPinchSpread()
    {
        if (gesture_finger1.phase == TouchPhase.Moved || gesture_finger2.phase == TouchPhase.Moved)
        {
            Vector2 prev1 = getPrevPoint(gesture_finger1);
            Vector2 prev2 = getPrevPoint(gesture_finger2);

            float currentDist = Vector2.Distance(gesture_finger1.position, gesture_finger2.position);
            float prevDist = Vector2.Distance(prev1, prev2);

            float diff = currentDist - prevDist;
            if (Mathf.Abs(diff) >= _pinchSpread.MinChange * Screen.dpi)
            {
                Debug.Log($"Pinch/Spread! {diff}");
                firePinchSpreadEvent();
            }
        }
    }

    private void checkForShake()
    {
        phoneAccel = Input.acceleration;
        if (phoneAccel.sqrMagnitude >= reloadTrigger && gunStatManager.bulletsLeft() < gunStatManager.getMaxAmmo())
        {
            Debug.Log("Shake!!");
            fireShakeEvent();
        }
    }

    private void fireSwipeEvent()
    {
        //Debug.Log("swipe!");
        Vector2 diff = end_pos - start_pos;

        //swiping left or right
        if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
        {        
            if (diff.x > 0)
            {
                Debug.Log("swiped right!");
                index++;
                if(index > gunStatManager.getGunCount() - 1)
                {
                    index = 0;
                }
            }
            else
            {
                Debug.Log("swiped left!");
                index--;
                if (index < 0)
                {
                    index = gunStatManager.getGunCount() - 1;
                }
            }
            gunStatManager.changeGun(index);
        }
    }

    private void firePinchSpreadEvent()
    {
        gunStatManager.nukeEnemies();
    }

    private void fireShakeEvent()
    {
        gunStatManager.reloadGun();
    }

    private Vector2 getPrevPoint(Touch t)
    {
        return t.position - t.deltaPosition;
    }

    public void Shoot()
    {
        gunStatManager.gunShot();
    }
}
