using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private float movementSpeedInZ = 1.0f;
    private Vector3 targetPos;
    public Transform finishLine;

    private void Start()
    {
        targetPos = generateTargetPos();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z > targetPos.z)
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

    private Vector3 generateTargetPos()
    {
        float x = Random.Range(-1.5f, 1.5f);
        return new Vector3(x, finishLine.position.y, finishLine.position.z);
    }
}
