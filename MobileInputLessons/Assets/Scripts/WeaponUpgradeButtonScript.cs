using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgradeButtonScript : MonoBehaviour
{
    public GameObject weaponUpgradeButton;
    public GameObject redGunButton;
    public GameObject blueGunButton;
    public GameObject yellowGunButton;
    public GameObject weaponUpgradePanel;

    void Start()
    {
        weaponUpgradeButton.SetActive(true);
        weaponUpgradePanel.SetActive(false);
    }

    public void ToggleWeaponUpgradePanel()
    {
        if (!weaponUpgradePanel.active)
        {
            weaponUpgradePanel.SetActive(true);
        }

        else if (weaponUpgradePanel.active)
        {
            weaponUpgradePanel.SetActive(false);
        }
    }

    public void UpgradeRedGunDamage()
    {
        // increase red gun damage IF PLAYER HAS ENOUGH POINTS
        Debug.Log("UPGRADED RED GUN DAMAGE!");
    }

    public void UpgradeBlueGunDamage()
    {
        // increase blue gun damage IF PLAYER HAS ENOUGH POINTS
        Debug.Log("UPGRADED BLUE GUN DAMAGE!");
    }

    public void UpgradeYellowGunDamage()
    {
        // increase yellow gun damage IF PLAYER HAS ENOUGH POINTS
        Debug.Log("UPGRADED YELLOW GUN DAMAGE!");
    }

}
