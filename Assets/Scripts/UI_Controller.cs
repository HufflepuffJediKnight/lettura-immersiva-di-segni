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
    public Animator animationController;
    public Button button1;
    public Button button2;
    public Button pauseButton;
    public Button playButton;

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
    private void Update()
    {
        CheckInputMethod();
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
        animationController.enabled = false;
        button.image.color = new Color(0, 0, 0, 0);
        animationController.enabled = true;
    }

    public void CheckInputMethod()
    {
        if (Input.touchCount != 0)
        {
            ColorBlock cb = pauseButton.colors;
            cb.normalColor = new Color(250, 250, 250);
            pauseButton.colors = cb;
            pauseButton.transform.localScale = new Vector3(2, 2, 2);
            pauseButton.transform.position = new Vector3(0, -440, 0);
            playButton.transform.localScale = new Vector3(2, 2, 2);
            playButton.transform.position = new Vector3(0, -440, 0);
        }
    }
}
