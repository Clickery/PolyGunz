using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public GameObject playerStats;
    public Joystick joystick;
    public Transform crossHair;
    private float sensitivity = 1000.0f;

    private GameObject gunStatManager;


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



    public GameObject[] guns;
    private int index;

    private void Start()
    {
        index = 0;

        for (int i = 0; i < guns.Length; i++)
        {
            if(i == index)
            {
                guns[i].SetActive(true);
            }
            else
            {
                guns[i].SetActive(false);
            }
        }

        gunStatManager = GameObject.FindGameObjectWithTag("GunStatsManager");

    }

    // Update is called once per frame
    private void Update()
    {
        //aiming
        aimCrossHair();

        //changing gun type
        if (Input.touchCount == 1)
        {
            gesture_finger1 = Input.GetTouch(0);
            checkForSwipe();
        }
        else if(Input.touchCount > 1)
        {
            gesture_finger1 = Input.GetTouch(0);
            gesture_finger2 = Input.GetTouch(1);

            if(gesture_finger1.phase == TouchPhase.Moved || gesture_finger2.phase == TouchPhase.Moved)
            {
                Vector2 prev1 = getPrevPoint(gesture_finger2);
                Vector2 prev2 = getPrevPoint(gesture_finger2);

                float currentDist = Vector2.Distance(gesture_finger1.position, gesture_finger2.position);
                float prevDist = Vector2.Distance(prev1, prev2);

                float diff = currentDist - prevDist;
                if(Mathf.Abs(diff) >= _pinchSpread.MinChange * Screen.dpi)
                {
                    Debug.Log("Pinch/Spread!");
                }
            }
        }

        //Reload
        phoneAccel = Input.acceleration;
        int maxAmmo = gunStatManager.GetComponent<GunStatsManager>().getMaxAmmo();
        int currentAmmo = gunStatManager.GetComponent<GunStatsManager>().bulletsLeft();
        if (phoneAccel.sqrMagnitude >= reloadTrigger && currentAmmo < maxAmmo)
        {
            reloadGun();
        }

    }

    private void aimCrossHair()
    {
        crossHair.position += new Vector3(joystick.Horizontal * sensitivity * Time.deltaTime, joystick.Vertical * sensitivity * Time.deltaTime, 0);
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

    private void fireSwipeEvent()
    {
        //Debug.Log("swipe!");
        Vector2 diff = end_pos - start_pos;

        //swiping left or right
        if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
        {

            guns[index].SetActive(false);
            if (diff.x > 0)
            {
                Debug.Log("swiped right!");
                index++;
                if(index > guns.Length - 1)
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
                    index = guns.Length - 1;
                }
            }
            guns[index].SetActive(true);
        }
    }

    private void reloadGun()
    {
        gunStatManager.GetComponent<GunStatsManager>().reloadGun();
    }

    private Vector2 getPrevPoint(Touch t)
    {
        return t.position - t.deltaPosition;
    }


    public void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(crossHair.position);
        RaycastHit enemyHit;

        int bulletsLeft = gunStatManager.GetComponent<GunStatsManager>().bulletsLeft();
        if(bulletsLeft > 0)
        {
            gunStatManager.GetComponent<GunStatsManager>().gunShot();//reduces ammo
            if (Physics.Raycast(ray, out enemyHit))
            {
                if (enemyHit.collider != null && enemyHit.collider.tag == "Enemy")
                {
                    //Debug.Log("3D Hit: " + enemyHit.collider.name);
                      
                    playerStats.GetComponent<PlayerStatsManager>().AddPoints();
                    playerStats.GetComponent<PlayerStatsManager>().AddScore();
                    enemyHit.collider.gameObject.GetComponent<EnemyBehavior>().onGetHit(playerStats.GetComponent<PlayerStatsManager>().GetDamage());
                }
            }
        }
        else
        {
            Debug.Log("Out of Ammo");
        }
    }
}
