using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public Transform gunParent;
    public Transform finishLineParent;

    // Start is called before the first frame update
    void Start()
    {
        spawnFinishLine();
        spawnGuns();
    }

    private void spawnFinishLine()
    {
        GameObject finishLine =  BundleManager.instance.GetAsset<GameObject>("objects", "FinishLine");
        Instantiate(finishLine, finishLineParent);
    }

    private void spawnGuns()
    {
        GameObject gun = BundleManager.instance.GetAsset<GameObject>("objects", "Handgun_Blue Variant");
        Instantiate(gun, gunParent);
        gun = BundleManager.instance.GetAsset<GameObject>("objects", "Handgun_Yellow Variant");
        Instantiate(gun, gunParent);
        gun = BundleManager.instance.GetAsset<GameObject>("objects", "Handgun_Red Variant");
        Instantiate(gun, gunParent);
    }

    
}
