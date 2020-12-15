using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Joystick joystick;
    public Transform crossHair;
    private float sensitivity = 1000.0f;

    // Update is called once per frame
    void Update()
    {
        aimCrossHair();
    }

    private void aimCrossHair()
    {
        crossHair.position += new Vector3(joystick.Horizontal * sensitivity * Time.deltaTime, joystick.Vertical * sensitivity * Time.deltaTime, 0);
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
                    enemyHit.collider.gameObject.GetComponent<EnemyBehavior>().onGetHit(1);
                }
            }
        }
    }
}
