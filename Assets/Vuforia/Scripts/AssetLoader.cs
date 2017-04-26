using UnityEngine;
using System.Collections;
using System;

public class AssetLoader : MonoBehaviour {

    public string BundelURL;
    public string AssetName;
    public int version = 1;
   // public Material icon_mat;
    AssetBundle bundle;


    void Start()
    {
        StartCoroutine(DownloadAndCache());
    }

    IEnumerator DownloadAndCache()
    {
        yield return StartCoroutine(AssetBundleManager.downloadAssetBundle(BundelURL, version));

        bundle = AssetBundleManager.getAssetBundle(BundelURL, version);

        GameObject cat = Instantiate(bundle.LoadAsset("bathroomcleaner") as GameObject);
        cat.transform.parent = GameObject.Find("ImageTarget").transform;
        // GameObject mat = cat.transform.GetChild(0).gameObject;
        cat.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        cat.transform.rotation = Quaternion.Euler(-90f, -180f, 0f);
        cat.transform.position = new Vector3(0.0f, 0.0f, -0.22f);


      //  mat.GetComponent<Renderer>().material = icon_mat;

        bundle.Unload(false);
    }
}
