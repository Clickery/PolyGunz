using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehavior : MonoBehaviour
{
    private float movementSpeedInZ = 1.0f;
    private Vector3 targetPos;
    private Transform finishLine;
    public HealthBar hpBar;

   
    //enemy stats
    private float nHp;
    //public int score = 100;

    public GameObject gameOverPanel;

    //anim purposes
    private Animator animator;
    private float timeDelay = 0.1f;
    private float tick = 0.0f;
    private float deathTimer = 0.0f;
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
        finishLine = BundleManager.instance.GetAsset<GameObject>("objects", "FinishLine").transform;

        animator = gameObject.GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if(!PersistentData.instance.isGamePaused())
        {
            tick += Time.deltaTime;
            if (tick >= timeDelay)
            {
                tick = 0.0f;
                if (!animator.GetBool("isDead"))
                {
                    animator.SetBool("isHit", false);
                }
            }

            if (animator.GetBool("isDead"))
            {
                deathTimer += Time.deltaTime;
                if (deathTimer >= 1.5f)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                moveForward();
            }

        }
    }

    private Vector3 generateTargetPos()
    {
        finishLine = BundleManager.instance.GetAsset<GameObject>("objects", "FinishLine").transform;
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
        hpBar.setHealth(tempHp);
        if (nHp > 0)
        {
            animator.SetBool("isHit", true);
        }
        else
        {
            animator.SetBool("isDead", true);
            Destroy(gameObject.GetComponent<Collider>());
        }
        
    }
}
