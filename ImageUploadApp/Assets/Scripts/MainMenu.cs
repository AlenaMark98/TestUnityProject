using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private ImageUpload ImgUpload;
    public List<Image> picture = new List<Image>();

    void Start()
    {

    }
    void Update()
    {
        
    }

    public void BTLoadImage() 
    {       
        for (int i = 0; i < picture.Count; i++)
        {
            ImgUpload = picture[i].GetComponent<ImageUpload>();
            ImgUpload.StartCoroutine(ImgUpload.LoadImage());
            Debug.Log("BTLoad");
        }
    }
}
