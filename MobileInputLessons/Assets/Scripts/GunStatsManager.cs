using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStatsManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] guns;
    private GameObject player;

    private int bulletCount;
    private int maxAmmo = 5;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bulletCount = maxAmmo;

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void gunShot()
    {
        bulletCount--;
    }

    public void reloadGun()
    {
        bulletCount = 5;
    }

    public int bulletsLeft()
    {
        return bulletCount;
    }

}
