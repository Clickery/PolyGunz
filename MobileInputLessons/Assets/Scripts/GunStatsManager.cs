﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunStatsManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource reloadSFX;
    public GameObject[] guns;
    private GameObject player;

    private int bulletCount;
    private int maxAmmo = 5;

    private bool nuke = true;

    public Text ammoText;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bulletCount = maxAmmo;

    }

    // Update is called once per frame
    void Update()
    {
        ammoText.text = "Ammo: " + bulletCount.ToString();
    }

    public void gunShot()
    {
        bulletCount--;
    }

    public void reloadGun()
    {
        reloadSFX.Play();
        bulletCount = maxAmmo;
    }

    public void nukeEnemies()
    {
        GameObject[] currentEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < currentEnemies.Length - 1; i++)
        {
            Destroy(currentEnemies[i]);
        }
        nuke = false;
    }

    public bool hasNuke()
    {
        return nuke;
    }

    public int bulletsLeft()
    {
        return bulletCount;
    }

    public int getMaxAmmo()
    {
        return maxAmmo;
    }

}
