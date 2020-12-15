using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public Transform crosshairPos;


    public void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(crosshairPos.position);
        RaycastHit enemyHit;
        if(Physics.Raycast(ray, out enemyHit))
        {
            if(enemyHit.collider != null)
            {
                if(enemyHit.collider.tag == "Enemy")
                {
                    Debug.Log("3D Hit: " + enemyHit.collider.name);
                    enemyHit.collider.gameObject.GetComponent<EnemyBehavior>().onGetHit();
                }
            }
        }
    }
}
