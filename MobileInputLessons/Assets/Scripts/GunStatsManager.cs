using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunStatsManager : MonoBehaviour
{
    //gun variables
    [System.Serializable]
    public class Gun
    {
        public string tag;
        public GameObject mesh;
        public int bullet_Count;
        public int max_Ammo;
        public int damage;
    }
    public Gun[] allGuns;
    private Gun currentGun;
    private int currentGunIndex = 0;
    private int maxAmmo = 5;
    ///////////////////////
    

    public Text nukeText;
    public AudioSource nukeSFX;
    public AudioSource reloadSFX;
    
    public RectTransform crossHair;
    public PlayerStatsManager playerStats;
    //public PlayerControls player;

    private bool nuke = true;

    public Text ammoText;
    public Text damageText;

    void Start()
    {
        allGuns[0].mesh = GameObject.Find("Handgun_Red Variant(Clone)");
        allGuns[1].mesh = GameObject.Find("Handgun_Blue Variant(Clone)");
        allGuns[2].mesh = GameObject.Find("Handgun_Yellow Variant(Clone)");

        for (int i = 0; i < allGuns.Length; i++)
        {
            allGuns[i].max_Ammo = maxAmmo;
            allGuns[i].bullet_Count = allGuns[i].max_Ammo;
            allGuns[i].mesh.SetActive(false);
            allGuns[i].damage = 1;
        }
        currentGun = allGuns[currentGunIndex];
        currentGun.mesh.SetActive(true);
    }

    public void gunShot()
    {
        Ray ray = Camera.main.ScreenPointToRay(crossHair.position);
        RaycastHit enemyHit;

        if (currentGun.bullet_Count > 0)
        {
            currentGun.bullet_Count--;
            if (Physics.Raycast(ray, out enemyHit) && enemyHit.collider != null)
            {
                if (enemyHit.collider.gameObject.CompareTag(currentGun.tag) || enemyHit.collider.gameObject.CompareTag("Boss"))
                {
                    //Debug.Log("3D Hit: " + enemyHit.collider.name);
                    playerStats.AddPoints();
                    playerStats.AddScore();
                    enemyHit.collider.gameObject.GetComponent<EnemyBehavior>().onGetHit(currentGun.damage);
                }
            }
        }
        else
        {
            Debug.Log("Out of Ammo");
        }
        //update ui text
        ammoText.text = "Ammo: " + currentGun.bullet_Count.ToString();
    }

    public void reloadGun()
    {
        reloadSFX.Play();
        currentGun.bullet_Count = currentGun.max_Ammo;
        //update ui text
        ammoText.text = "Ammo: " + currentGun.bullet_Count.ToString();
    }

    public void nukeEnemies()
    {
        nukeSFX.Play();
        EnemyBehavior[] currentEnemies = GameObject.FindObjectsOfType<EnemyBehavior>();
        for(int i = 0; i < currentEnemies.Length; i++)
        {
            currentEnemies[i].gameObject.GetComponent<Animator>().SetBool("isDead", true);
        }
        nuke = false;
        nukeText.text = "x 0";
    }


    public void changeGun(int index)
    {
        if(index <= allGuns.Length && index >= 0)
        {
            for(int i = 0; i < allGuns.Length; i++)
            {
                allGuns[i].mesh.SetActive(false);
            }
            currentGun = allGuns[index];
            currentGun.mesh.SetActive(true);
        }
        ammoText.text = "Ammo: " + currentGun.bullet_Count.ToString();
        damageText.text = "Damage: " + currentGun.damage.ToString();
    }

    public void upgradeWeapon(int index)
    {
        allGuns[index].damage += 1;
    }

    public int getGunCount()
    {
        return allGuns.Length;
    }

    public bool hasNuke()
    {
        return nuke;
    }

    public int bulletsLeft()
    {
        return currentGun.bullet_Count;
    }

    public int getMaxAmmo()
    {
        return currentGun.max_Ammo;
    }

    public Gun GetGun()
    {
        return currentGun;
    }

}
