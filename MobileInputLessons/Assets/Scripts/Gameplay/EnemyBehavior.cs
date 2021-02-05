using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehavior : MonoBehaviour
{
    private float movementSpeedInZ = 1.0f;
    private Vector3 targetPos;
    public Transform finishLine;
    public HealthBar hpBar;

    //enemy stats
    private float nHp;
    //public int score = 100;

    public GameObject gameOverPanel;

    // Start is called before the first frame update

    private void Awake()
    {
        //set enemy Hp
        if (this.gameObject.CompareTag("Boss"))
        {
            nHp = 20.0f;
        }
        else
        {
            nHp = 5.0f;
        }

        int tempHp = (int)nHp;
        hpBar.setMaxHealth(tempHp);

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
            SceneManager.LoadScene(2);
            Destroy(this.gameObject);
        }
    }

    public void increaseStats(float multiplier)
    {
        nHp *= multiplier;
        int tempHp = (int)nHp;
        hpBar.setHealth(tempHp);
    }

    public void onGetHit(int damage)
    {
        //deduct hp for enemy

        nHp -= damage;
        nHp = Mathf.Round(nHp);
        Debug.Log(this.name + " has " + nHp + " HP");
        int tempHp = (int)nHp;
        if (nHp > 0)
        {
            hpBar.setHealth(tempHp);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }
}
