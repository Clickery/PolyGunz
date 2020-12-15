using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStatsManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] guns;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
