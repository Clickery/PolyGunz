using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgradeButton : MonoBehaviour
{
    public GameObject weaponUpgradeToggle;
    public GameObject weaponUpgradePanel;


    // Start is called before the first frame update
    void Start()
    {
        weaponUpgradeToggle.SetActive(true);
        weaponUpgradePanel.SetActive(false);
    }

    public void ShowWeaponUpgradePanel()
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

}
