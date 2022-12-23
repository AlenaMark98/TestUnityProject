using UnityEngine;
using UnityEngine.Networking;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Linq;

public static class AsyncAwait
{
    public static CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

    public static async void GetLoadTextureAsync(string url, Action<Texture2D> onSuccess)
    {
        onSuccess(await LoadTextureAsync(url));
        Debug.Log($"Texture completed! id {Thread.CurrentThread.ManagedThreadId}");
    }

    public static async Task GetLoadOneByOneTextureAsync(string url, Action<Texture2D> onSuccess)
    {
        onSuccess(await LoadTextureAsync(url));
        Debug.Log($"Texture completed! id {Thread.CurrentThread.ManagedThreadId}");
    }

    public static async void GetLoadAllTextureAsync(string[] url2, Action<Texture2D[]> onSuccess)
    {
        onSuccess(await Task.WhenAll(url2.Select(LoadTextureAsync)));
        Debug.Log($"All texture completed! id {Thread.CurrentThread.ManagedThreadId}");
    }

    private static async Task<Texture2D> LoadTextureAsync(string url)
    {
        using UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture(url);
        
        CancellationToken token = cancelTokenSource.Token;
      
        var operation = unityWebRequest.SendWebRequest();

        while (operation.isDone == false)
        {
            if (token.IsCancellationRequested)
            {
                Debug.Log("Task {0} cancelled");
                token.ThrowIfCancellationRequested();
            }
            await Task.Yield();
        }
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
