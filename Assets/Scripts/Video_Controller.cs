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
        currentPlayer.loopPointReached += AutoCloseHotspots;
    }

    private void AutoCloseHotspots(VideoPlayer source)
    {
        for (int i = 0; i < Hotspots.Length; i++)
        {
            Hotspots[i].SetActive(false);
        }
    }

    public void CheckVideoStatus()
    {
        for (int i = 0; i < videoPlayers.Length; i++)
        {
            if (videoPlayers[i].isPlaying)
            {
                videoPlayers[i] = currentPlayer;
            }
        }
    }

}
