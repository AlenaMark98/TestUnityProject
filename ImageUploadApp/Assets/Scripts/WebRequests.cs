using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public static class WebRequests 
{
    private class WebRequestsMonoBehaviour : MonoBehaviour { }

    private static WebRequestsMonoBehaviour webRequestsMonoBehaviour;

    private static void Init() 
    {
        if (webRequestsMonoBehaviour == null)
        {
            GameObject gameObj = new GameObject("WebRequests");
            webRequestsMonoBehaviour = gameObj.AddComponent<WebRequestsMonoBehaviour>();
        }
    }

    public static void GetTextureCoroutine(string url, Action<string> onError, Action<Texture2D> onSuccess) 
    {
        Init();
        webRequestsMonoBehaviour.StartCoroutine(LoadTextureCoroutine(url, onError, onSuccess));
    }

    private static IEnumerator LoadTextureCoroutine(string url, Action<string> onError, Action<Texture2D> onSuccess)
    {
        UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture(url);

        yield return unityWebRequest.SendWebRequest();
        if (unityWebRequest.isDone == false)
        {
            onError(unityWebRequest.error);
        }
        else
        {
            DownloadHandlerTexture downloadHandlerTexture = unityWebRequest.downloadHandler as DownloadHandlerTexture;
            onSuccess(downloadHandlerTexture.texture);
        }
    }


}
