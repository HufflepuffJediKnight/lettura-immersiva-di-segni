using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System;
using static UI_Controller;

public class Video_Controller : MonoBehaviour
{
    public VideoPlayer[] videoPlayers;
    VideoPlayer currentPlayer;
    public GameObject[] Hotspots;
    public UI_Controller controller;
    public GameObject pauseButton;

    void PrepareVideos()
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
            }
        }
    }

    // disattiva entrambi gli hotspots (anche se uno dei due sarà già disattivo)
    // che prende da un array di hotspot
    private void AutoCloseHotspots(VideoPlayer source)
    {
        for (int i = 0; i < Hotspots.Length; i++)
        {
            Hotspots[i].SetActive(false);
        }
        controller.Play();
    }

}
