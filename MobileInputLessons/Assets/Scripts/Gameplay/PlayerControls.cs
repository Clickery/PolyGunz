using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public GameObject playerStats;
    public Joystick joystick;
    public Transform crossHair;
    private float sensitivity = 1000.0f;


    //Touch purposes/////////////////////
    public SwipeProperty _swipeProperty;
    //finger touch
    private Touch gesture_finger1;
    private Vector2 start_pos;
    private Vector2 end_pos;
    private float gesture_time;
    //Touch purposes/////////////////////

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
        
    }


    // Update is called once per frame
    private void Update()
    {
        aimCrossHair();
        if (Input.touchCount == 1)
        {
            checkForSwipe();
        }
    }

    private void aimCrossHair()
    {
        crossHair.position += new Vector3(joystick.Horizontal * sensitivity * Time.deltaTime, joystick.Vertical * sensitivity * Time.deltaTime, 0);
    }


    private void checkForSwipe()
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



    public void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(crossHair.position);
        RaycastHit enemyHit;
        if (Physics.Raycast(ray, out enemyHit))
        {
            if (enemyHit.collider != null)
            {
                if (enemyHit.collider.tag == "Enemy")
                {
                    Debug.Log("3D Hit: " + enemyHit.collider.name);
                    playerStats.GetComponent<PlayerStatsManager>().AddPoints();
                    playerStats.GetComponent<PlayerStatsManager>().AddScore();
                    enemyHit.collider.gameObject.GetComponent<EnemyBehavior>().onGetHit(1);
                }
            }
        }
    }
}
