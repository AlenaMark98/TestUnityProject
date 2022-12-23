using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadImage : MonoBehaviour
{
    [SerializeField] private string _url = "https://picsum.photos/300";
    [SerializeField] private string[] _urlArray = {"https://picsum.photos/300"};

   
    public List<GameObject> pictureCardPrefabs = new List<GameObject>();
    private PictureCard pictureCard;

    [SerializeField] private Button _bTLoad;
    [SerializeField] private Button _bTCancel;
    [SerializeField] private TMP_Dropdown _dropdown;

    private bool AllAtOnce = true;
    private bool OneByOne = false;
    private bool WhenImageReady = false;

    void Start()
    {
        StartCoroutine(interactableBTCancelCoroutine(false));
    }

    public void BTCancel()
    { 
        if (AsyncAwait.cancelTokenSource != null)
        {
            AsyncAwait.cancelTokenSource.Cancel();
            AsyncAwait.cancelTokenSource.Dispose();
            AsyncAwait.cancelTokenSource = null;
        }
    }

    public async void BTLoadImage() 
    {
        for (int i = 0; i < pictureCardPrefabs.Count; i++)
        {
            pictureCard = pictureCardPrefabs[i].GetComponent<PictureCard>();
            pictureCard.openCard(false);
        }
        
        if (AllAtOnce)
        {
            Debug.Log("BTLoad AllAtOnce");
            StartCoroutine(interactableBTCancelCoroutine(true));

            AsyncAwait.GetLoadAllTextureAsync(_urlArray, (Texture2D[] texture2D) =>
            {
                for (int i = 0; i < texture2D.Length; i++)
                {
                    pictureCard = pictureCardPrefabs[i].GetComponent<PictureCard>();
                    Sprite sprite = Sprite.Create(texture2D[i], new Rect(0, 0, texture2D[i].width, texture2D[i].height), Vector2.zero);
                    pictureCard.SetSpritePicture(sprite);
                    pictureCard.openCard(true);
                }
                StartCoroutine(interactableBTCancelCoroutine(false));
            });            
        }
        else if (OneByOne)
        {
            Debug.Log("BTLoad OneByOne");
            StartCoroutine(interactableBTCancelCoroutine(true));

            for (int i = 0; i < pictureCardPrefabs.Count; i++)
            {
                pictureCard = pictureCardPrefabs[i].GetComponent<PictureCard>();
                await AsyncAwait.GetLoadOneByOneTextureAsync(_url, (Texture2D texture2D) =>
                {
                    Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.zero);
                    pictureCard.SetSpritePicture(sprite);
                    pictureCard.openCard(true);
                });
            }
            StartCoroutine(interactableBTCancelCoroutine(false));
        }
        else if (WhenImageReady) 
        {
            Debug.Log("BTLoad WhenImageReady");
            StartCoroutine(interactableBTCancelCoroutine(true));

            for (int i = 0; i < pictureCardPrefabs.Count; i++)
            {
                pictureCard = pictureCardPrefabs[i].GetComponent<PictureCard>();
                pictureCard.WhenImageReady();
            }
        }
    }

    public IEnumerator interactableBTCancelCoroutine(bool isCancel)
    {
        if (isCancel)
        {
            _bTLoad.interactable = false;
            _dropdown.interactable = false;
            _bTCancel.interactable = true;
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            _bTLoad.interactable = true;
            _dropdown.interactable = true;
            _bTCancel.interactable = false;
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void Dropdown(int value)
    {
        switch (value)
        {
            case 0:
                AllAtOnce = true;
                OneByOne = false;
                WhenImageReady = false;
                break;
            case 1:
                OneByOne = true;
                AllAtOnce = false;
                WhenImageReady = false;
                break;
            case 2:
                WhenImageReady = true;
                AllAtOnce = false;
                OneByOne = false;
                break;
            default:
                AllAtOnce = true;
                OneByOne = false;
                WhenImageReady = false;
                break;
        }
    }


}
