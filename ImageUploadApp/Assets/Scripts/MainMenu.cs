using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private ImageUpload ImgUpload;
    public Image photo;

    void Start()
    {
        //ImgUpload = GameObject.Find("Photo").GetComponent<ImageUpload>();
        ImgUpload = photo.GetComponent<ImageUpload>();
    }

    void Update()
    {
        
    }

    public void BTLoadImage() 
    {
        ImgUpload.StartCoroutine(ImgUpload.LoadImage());
    }
}
