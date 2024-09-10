using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.Rendering.PostProcessing;
using System;

public class UI_Controller : MonoBehaviour
{
    int currentScene;
    public Button button1;
    public Button button2;

/*
    public void Fullscreen()
    {
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
    }

    public void FullscreenExit()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
    }
*/
    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        button1.onClick.AddListener(delegate { ButtonDestroyer(button1); });
        button2.onClick.AddListener(delegate { ButtonDestroyer(button2); });
    }

    public void Pause(UI_Controller controller)
    {
        Time.timeScale = 0.0f;
    }

    public void Play()
    {
        Time.timeScale = 1.0f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void Blur(bool isBlurred)
    {
        PostProcessVolume postVolume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
        if (!isBlurred)
        {
            postVolume.enabled = true;
            isBlurred = true;
        }
        else
        {
            postVolume.enabled = false;
        }
        
    }

    public void ButtonDestroyer(Button button)
    {
        Button.Destroy(button);
        button.image.color = new Color(0, 0, 0, 0);
    }

}
