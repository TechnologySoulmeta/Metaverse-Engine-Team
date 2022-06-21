IEnumerator loadAsset(string name, string url)
{

    using (UnityWebRequest web = UnityWebRequestAssetBundle.GetAssetBundle(url))
    {
        yield return web.SendWebRequest();
        if (web.result == UnityWebRequest.Result.Success)
        {
            AssetBundle remoteAssetBundle = (web.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
            if (remoteAssetBundle == null)
            {
                Debug.LogError("Failed to load");
                loading.GetComponent<TextMesh>().text = "Network error 01!";
                yield break;
            }
            else
            {
                Destroy(loading);
                GameObject a = (GameObject)Instantiate(remoteAssetBundle.LoadAsset(name));
                a.transform.SetParent(target);
                a.transform.localPosition = Vector3.zero;
                Debug.Log(a.transform.parent.name);
                ani2 = a.GetComponent<Animator>();
                remoteAssetBundle.Unload(false);
            }

        }
        else
        {
            loading.GetComponent<TextMesh>().text = "Network error 02!";
        }
    }
}