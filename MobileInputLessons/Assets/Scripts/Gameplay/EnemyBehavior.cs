﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    //public GameObject playerStats;

    private float movementSpeedInZ = 1.0f;
    private Vector3 targetPos;
    public Transform finishLine;

    //enemy stats
    private int nHp;


    // Start is called before the first frame update
    void Start()
    {
        targetPos = generateTargetPos();
    }

    // Update is called once per frame
    void Update()
    {
        moveForward();
    }

    private Vector3 generateTargetPos()
    {
        float x = Random.Range(-1.5f, 1.5f);
        return new Vector3(x, finishLine.position.y, finishLine.position.z);
    }

    private void moveForward()
    {
        if (transform.position.z > targetPos.z)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, movementSpeedInZ * Time.deltaTime);
            //Debug.Log(transform.position.z);
        }
        else
        {
            Debug.Log("This should be dead!");
            Destroy(this.gameObject);
        }
    }

    public void onGetHit()
    {
        //deduct hp for enemy
<<<<<<< HEAD
        
=======
        //playerStats.GetComponent<PlayerStatsManager>().AddPoints();
        //playerStats.GetComponent<PlayerStatsManager>().AddScore();
>>>>>>> 11253aca0ae9ad4cec8d6e10628c8e5ea4c6e934
        Destroy(this.gameObject);
    }
}
