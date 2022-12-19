using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ImageUpload : MonoBehaviour
{
    [SerializeField] private string _url;

    private Image _img;

    void Start()
    {
        _img = GetComponent<Image>();

        //StartCoroutine(LoadImage());
    }

    public IEnumerator LoadImage()
    {
        UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(_url);

        yield return webRequest.SendWebRequest();
        if (webRequest.isDone == false)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            Texture texture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
            _img.sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            Debug.Log(_img.sprite.name);
        }

    }

}
