using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.Rendering;
using System;

public class Video_Controller : MonoBehaviour
{
    public VideoPlayer[] videoPlayers;
    VideoPlayer currentPlayer;
    public VideoPlayer mainPlayer;
    public AnimationClip mainAnimation;
    public GameObject[] Hotspots;
    public GameObject pauseButton;
    public GameObject restartButton;
    public Button menuButton;
    public GameObject menuCanvas;
    public UI_Controller controller;

    /*
        public void PrepareVideos()
        {
            for (int i = 0; i < videoPlayers.Length; i++) GetComponent<VideoPlayer>().Prepare();
        }
    */

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
        //        PrepareVideos();
        if (SplashScreen.isFinished)
        {
            mainPlayer.Play();
            controller.Play();
        }
    }

    private void Update()
    {
        CheckVideoStatus();

    }

    //controlla se uno dei due videoplayer degli hotspot sta riproducendo un video
    //se sì lo assegna alla variabile currentPlayer e alla fine della riproduzione
    //attiverà la funzione AutoCloseHotspots
    public void CheckVideoStatus()
    {
        foreach (var i in videoPlayers)
        {
            if (i.isPlaying)
            {
                currentPlayer = i;
                currentPlayer.loopPointReached += AutoCloseHotspots;
                menuButton.enabled = false;
            }
        }
        mainPlayer.loopPointReached += AutoCloseButtons;
    }

    // disattiva entrambi gli hotspots (anche se uno dei due sarà già disattivo)
    // che prende da un array di hotspot
    public void AutoCloseHotspots(VideoPlayer player)
    {
        try
        {
            foreach (var i in Hotspots)
            {
                i.SetActive(false);
            }
            controller.Play();
            controller.Blur(true);
            pauseButton.SetActive(true);
            menuButton.enabled = true;
        }
        catch
        {
            throw new NotImplementedException();
        }
    }

    public void AutoCloseButtons(VideoPlayer player)
    {
        pauseButton.SetActive(false);
        menuButton.gameObject.SetActive(false);
        restartButton.SetActive(true);
        menuCanvas.SetActive(false);
    }

}
