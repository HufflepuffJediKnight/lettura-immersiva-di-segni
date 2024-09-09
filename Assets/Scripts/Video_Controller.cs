using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System;

public class Video_Controller : MonoBehaviour
{
    public VideoPlayer[] videoPlayers;
    VideoPlayer currentPlayer;
    public GameObject[] Hotspots;
    public GameObject pauseButton;
    public Button menuButton;
    public UI_Controller controller;

    public void PrepareVideos()
    {
        for (int i = 0; i < videoPlayers.Length; i++) GetComponent<VideoPlayer>().Prepare();
    }
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
        PrepareVideos();
    }

    private void Update()
    {
        CheckVideoStatus();
        
    }

    //controlla se uno dei due videoplayer degli hotspot sta riproducendo un video
    //se s� lo assegna alla variabile currentPlayer e alla fine della riproduzione
    //attiver� la funzione AutoCloseHotspots
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
    }

    // disattiva entrambi gli hotspots (anche se uno dei due sar� gi� disattivo)
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

}
