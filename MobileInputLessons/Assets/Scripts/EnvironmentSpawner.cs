using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        loadEnvironment();
    }

    private void loadEnvironment()
    {
        GameObject environment = BundleManager.instance.GetAsset<GameObject>("environment", "StageEnvironment");
        Instantiate(environment, transform);
    }
}
