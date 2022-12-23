using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Networking;

public class PictureCard : MonoBehaviour
{
    [SerializeField] private string _url = "https://picsum.photos/300";
    
    [SerializeField] private GameObject _picture;
    private Image _img;

    [SerializeField] private Transform _backCard;
    private Transform card;

    private Sequence Seq;

    private void Awake()
    {
        card = this.GetComponent<Transform>();
        card.transform.rotation *= Quaternion.Euler(0, 180, 0);
        _backCard.SetAsLastSibling();
    }

    void Start()
    {
        _img = _picture.GetComponent<Image>();
    }

    public void WhenImageReady()
    {
        openCard(false);

        //-------example Coroutine
        //WebRequests.GetTextureCoroutine(_url, (string error) =>
        //{
        //    Debug.Log("Error: " + error);
        //}, (Texture2D texture2D) =>
        //{
        //    Debug.Log("Success! ");
        //    Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.zero);
        //    _img.sprite = sprite;

        //    openCard(true);
        //});


        //---------example AsyncAwait
        AsyncAwait.GetLoadTextureAsync(_url, (Texture2D texture2D) =>
        {
            Debug.Log("Success! ");
            Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.zero);
            _img.sprite = sprite;
            openCard(true);
        });
       
    }


    public void openCard(bool open)
    {
        if (open)
        {
            card.DORotate(new Vector3(0, 90, 0), 0.3f, RotateMode.FastBeyond360).SetEase(Ease.Linear);
            _backCard.SetAsFirstSibling();
            card.DORotate(new Vector3(0, 0, 0), 0.3f, RotateMode.FastBeyond360).SetEase(Ease.Linear);
        }
        else 
        {
            card.DORotate(new Vector3(0, 90, 0), 0.3f, RotateMode.FastBeyond360).SetEase(Ease.Linear);
            _backCard.SetAsLastSibling();
            card.DORotate(new Vector3(0, 180, 0), 0.3f, RotateMode.FastBeyond360).SetEase(Ease.Linear);
        }
    }


}
