using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgradeButtonScript : MonoBehaviour
{
    public GameObject weaponUpgradeButton;
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

}
