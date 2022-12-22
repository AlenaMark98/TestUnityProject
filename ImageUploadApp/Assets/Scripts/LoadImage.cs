using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class LoadImage : MonoBehaviour
{
    [SerializeField] private string _url = "https://picsum.photos/300";
    [SerializeField] private string[] _url2 = {"https://picsum.photos/300"};

   
    public List<GameObject> pictureCardPrefabs = new List<GameObject>();
    private PictureCard pictureCard;

    public List<Image> imgPrefabs = new List<Image>();

    private bool AllAtOnce = true;
    private bool OneByOne = false;
    private bool WhenImageReady = false;
    public bool btCancel = false;


    public void BTLoadImage() 
    {
        if (AllAtOnce)
        {
            Debug.Log("BTLoad AllAtOnce");

            AsyncAwait.GetLoadAllTextureAsync(_url2, (Texture2D[] texture2D) =>
            {
                for (int i = 0; i < texture2D.Length; i++)
                {
                    pictureCard = pictureCardPrefabs[i].GetComponent<PictureCard>();
                    pictureCard.openCard(false);

                    Sprite sprite = Sprite.Create(texture2D[i], new Rect(0, 0, texture2D[i].width, texture2D[i].height), Vector2.zero);
                    imgPrefabs[i].sprite = sprite;
                    Debug.Log("Success i = " + i);

                    pictureCard.openCard(true);
                }
            });

        }
        else if (OneByOne)
        {
            Debug.Log("BTLoad OneByOne");     
            
        }
        else if (WhenImageReady) 
        {
            Debug.Log("BTLoad WhenImageReady");

            for (int i = 0; i < pictureCardPrefabs.Count; i++)
            {
                Debug.Log("pictureCardPrefabs i = " + i);
                pictureCard = pictureCardPrefabs[i].GetComponent<PictureCard>();
                pictureCard.WhenImageReady();
            }
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
