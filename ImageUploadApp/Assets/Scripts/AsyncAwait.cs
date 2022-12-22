using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Linq;

public static class AsyncAwait
{
    public static async void GetLoadTextureAsync(string url, Action<Texture2D> onSuccess)
    {
        onSuccess(await LoadTextureAsync(url));
        Debug.Log("All texture completed!");
    }


    public static async void GetLoadAllTextureAsync(string[] url2, Action<Texture2D[]> onSuccess)
    {
        onSuccess(await Task.WhenAll(url2.Select(LoadTextureAsync)));
    }

    public static async Task<Texture2D> LoadTextureAsync(string url)
    {
        using UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture(url);
        
        var operation = unityWebRequest.SendWebRequest();

        while (operation.isDone == false)
            await Task.Yield();

        if (unityWebRequest.result == UnityWebRequest.Result.Success)
        {
            DownloadHandlerTexture downloadHandlerTexture = unityWebRequest.downloadHandler as DownloadHandlerTexture;

            Debug.Log("Success!");
            return downloadHandlerTexture.texture;
        }
        else
        {
            Debug.Log("Error: " + unityWebRequest.error);
            return null;
        }
    }

}
