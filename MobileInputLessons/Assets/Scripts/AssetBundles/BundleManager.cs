using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class BundleManager : MonoBehaviour
{

    public static BundleManager instance = null;

    public string bundleRoot
    {
        get
        {
#if UNITY_EDITOR
            return Application.streamingAssetsPath;
#elif UNITY_ANDROID
            return Application.persistentDataPath;
#endif
        }
    }

    Dictionary<string, AssetBundle> LoadedBundles = new Dictionary<string, AssetBundle>();


    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    public AssetBundle LoadBundle(string bundleTarget)
    {
        if (LoadedBundles.ContainsKey(bundleTarget))
        {
            return LoadedBundles[bundleTarget];
        }

        AssetBundle ret = AssetBundle.LoadFromFile(Path.Combine(bundleRoot, bundleTarget));

        if(ret == null)
        {
            Debug.LogError($"{bundleTarget} does not exist");
        }
        else
        {
            LoadedBundles.Add(bundleTarget, ret);
        }

        return ret;
    }

    public T GetAsset<T>(string bundleTarget, string assetName) where T : UnityEngine.Object
    {
        T ret = null;
        AssetBundle bundle = LoadBundle(bundleTarget);

        if(bundle != null)
        {
            ret = bundle.LoadAsset<T>(assetName);
        }
        else
        {
            Debug.Log("failed loading asset!");
        }
        return ret;
    }
}
