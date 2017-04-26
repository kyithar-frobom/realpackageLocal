
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AssetBundleManager : MonoBehaviour {

    static private Dictionary<string, AssetBundleRef> dictAssetBundleRefs;
    static AssetBundleManager()
    {
        dictAssetBundleRefs = new Dictionary<string, AssetBundleRef>();

    }

    private class AssetBundleRef
    {
        public AssetBundle assetBundle = null;
        public int version;
        public string url;
        public AssetBundleRef(string strUrlIn, int intVersionIn)
        {
            url = strUrlIn;
            version = intVersionIn;
        }
    };

    public static AssetBundle getAssetBundle(string url, int version)
    {
        string keyName = url + version.ToString();
        AssetBundleRef abRef;
        if (dictAssetBundleRefs.TryGetValue(keyName, out abRef))
            return abRef.assetBundle;
        else
            return null;
    }

    public static IEnumerator downloadAssetBundle(string url, int version)
    {
        string keyName = url + version.ToString();
        using (WWW www = WWW.LoadFromCacheOrDownload(url, version))
        {
            yield return www;
            if (www.error != null)
            {
                AssetBundleManager.Unload(url, version, false);
                Debug.Log("Error :" + www.error);
                throw new Exception("WWW download:" + www.error);
            }
            AssetBundleRef abRef = new AssetBundleRef(url, version);
            abRef.assetBundle = www.assetBundle;
            if (!dictAssetBundleRefs.ContainsKey(keyName))
            {
                dictAssetBundleRefs.Add(keyName, abRef);
            }
            else
            {
                Debug.Log("This is Just Test that how we can unload asset which is in cache");
                AssetBundleManager.Unload(url, version, false);
            }

        }
    }

    public static void Unload(string url, int version, bool allObjects)
    {
        string keyName = url + version.ToString();
        AssetBundleRef abRef;
        if (dictAssetBundleRefs.TryGetValue(keyName, out abRef))
        {
            abRef.assetBundle.Unload(allObjects);
            abRef.assetBundle = null;
            dictAssetBundleRefs.Remove(keyName);
        }
    }
}
