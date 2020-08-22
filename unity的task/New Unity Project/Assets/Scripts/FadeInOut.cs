using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour
{
    private float fadeSpeed = 1.5f;

    private bool sceneStarting = true;
    public UnityEngine.UI.Image image;
    void Start()
    {
        //tex = this.GetComponent<GUITexture>();
        //tex.pixelInset = new Rect(0, 0, Screen.width, Screen.height);
    }

    void Update()
    {
        if(sceneStarting)
        {
            StartScene();
        }
    }



    private void FadeToClear()
    {
        image.color = Color.Lerp(image.color, Color.clear,fadeSpeed * Time.deltaTime);
    }

    private void FadeToBlack()
    {
        image.color = Color.Lerp(image.color, Color.black, fadeSpeed * Time.deltaTime);
    }

    private void StartScene()
    {
        FadeToClear();
        if(image.color.a <= 0.05f)
        {
            image.color = Color.clear;
            image.enabled = false;
            sceneStarting = false;
        }   
    }

    public void EndScen()
    {
        image.enabled = true;
        FadeToBlack();

        if(image.color.a >= 0.95f)
        {
            SceneManager.LoadScene("demo");
        }    
    }



}
