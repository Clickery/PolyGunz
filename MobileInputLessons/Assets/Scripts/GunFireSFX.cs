using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFireSFX : MonoBehaviour
{
    public GunStatsManager gunStatManager;

    public AudioSource gunFireWithBullets;
    public AudioSource gunFireWithoutBullets;

   

    public void PlayGunFireSFX()
    {
        if (gunStatManager.GetGun().bullet_Count == 0)
        {
            gunFireWithoutBullets.Play();
        }

        else if (gunStatManager.GetGun().bullet_Count > 0)
        {
            gunFireWithBullets.Play();
        }
    }
}
