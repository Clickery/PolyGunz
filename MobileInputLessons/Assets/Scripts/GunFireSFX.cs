using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFireSFX : MonoBehaviour
{
    private GameObject gunStatManager;
    private int bulletCount = 0;

    public AudioSource gunFireWithBullets;
    public AudioSource gunFireWithoutBullets;

    // Start is called before the first frame update
    void Start()
    {
        gunStatManager = GameObject.FindGameObjectWithTag("GunStatsManager");

        
    }

    // Update is called once per frame
    void Update()
    {
        bulletCount = gunStatManager.GetComponent<GunStatsManager>().bulletsLeft();
        
    }

    public void PlayGunFireSFX()
    {
        if (bulletCount <= 0)
        {
            gunFireWithoutBullets.Play();
        }

        else if (bulletCount > 0)
        {
            gunFireWithBullets.Play();
        }
    }
}
