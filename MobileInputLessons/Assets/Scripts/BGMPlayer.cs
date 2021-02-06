using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMPlayer : MonoBehaviour
{
    public static BGMPlayer instance = null;

    private AudioClip[] bgMusic = new AudioClip[3];
    private AudioSource source;

    private bool[] isPlaying = new bool[3];

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();

        
        bgMusic[0] = BundleManager.instance.GetAsset<AudioClip>("audio", "sawsquarenoise stage1");
        bgMusic[1] = BundleManager.instance.GetAsset<AudioClip>("audio", "sawsquarenoise stage2");
        bgMusic[2] = BundleManager.instance.GetAsset<AudioClip>("audio", "sawsquarenoise stage3");

        source.clip = bgMusic[0];
        source.Play();
        source.loop = true;
        isPlaying[0] = true;
        isPlaying[1] = false;
        isPlaying[2] = false;
    }


    private void Update()
    {
        //Debug.Log(isPlaying[0] + ", " + isPlaying[1] + ", " + isPlaying[2]);

        if(SceneManager.GetActiveScene().buildIndex == 0 && isPlaying[0] == false)
        {
            changeMusic(0);
            isPlaying[0] = true;
            isPlaying[1] = false;
            isPlaying[2] = false;

        }
        else if(SceneManager.GetActiveScene().buildIndex == 1 && isPlaying[1] == false)
        {
            changeMusic(1);
            isPlaying[0] = false;
            isPlaying[1] = true;
            isPlaying[2] = false;
        }
        else if(SceneManager.GetActiveScene().buildIndex == 2 && isPlaying[2] == false)
        {
            changeMusic(2);
            isPlaying[0] = false;
            isPlaying[1] = false;
            isPlaying[2] = true;
        }
    }


    public void changeMusic(int index)
    {
        source.Stop();
        source.clip = bgMusic[index];
        source.Play();
        source.loop = true;
    }

}
