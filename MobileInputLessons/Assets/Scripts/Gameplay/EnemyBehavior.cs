using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private float movementSpeedInZ = 1.0f;
    private Vector3 targetPos;
    public Transform finishLine;

    public HealthBar hpBar;

    //enemy stats
    private int nHp;
    public int score = 100;


    // Start is called before the first frame update
    void Start()
    {
        //set enemy Hp
        if(this.gameObject.name == "Boss(Clone)")
        {
            nHp = 20;
        }
        else
        {
            nHp = 5;
        }
        hpBar.setMaxHealth(nHp);

        //setting destination point
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

    public void onGetHit(int damage)
    {
        //deduct hp for enemy

        nHp -= damage;
        if(nHp > 0)
        {
            hpBar.setHealth(nHp);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }
}
