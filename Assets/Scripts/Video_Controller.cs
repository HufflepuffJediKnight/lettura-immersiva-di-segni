using UnityEngine;
using UnityEngine.Video;
using System;
using System.Linq;
using UnityEngine.UI;

public class Video_Controller : MonoBehaviour
{
    UI_Controller uiController;

    GameObject[] _videoPlayers;
    VideoPlayer _mainPlayer;
    GameObject[] _hotspots;

    VideoPlayer currentPlayer;

    public GameObject[] VideoPlayers
    {
        get { return _videoPlayers; }
        set
        {
            VideoPlayers.Append(MainPlayer.gameObject).ToArray();
            VideoPlayers.Append(GameObject.Find("Hotspot_1_Player")).ToArray();
            VideoPlayers.Append(GameObject.Find("Hotspot_2_Player")).ToArray();
        }
    }
    public VideoPlayer MainPlayer
    {
        get { return _mainPlayer; }
        set { _mainPlayer = GameObject.Find("Video_Player").GetComponent<VideoPlayer>(); }
    }
    public GameObject[] Hotspots
    {
        get { return _hotspots; }
        set
        {
            Hotspots = Hotspots.Append(GameObject.Find("Hotspot_1")).ToArray();
            Hotspots = Hotspots.Append(GameObject.Find("Hotspot_2")).ToArray();
        }
    }

    public void PrepareVideos()
    {
        for (int i = 0; i < VideoPlayers.Length; i++)
        {
            VideoPlayers[i].GetComponent<VideoPlayer>().Prepare();
        }
        MainPlayer.GetComponent<VideoPlayer>().Prepare();
    }

    private void Start()
    {
    }

    private void Update()
    {
        PrepareVideos();
        CheckVideoStatus();
    }

    //controlla se uno dei due videoplayer degli hotspot sta riproducendo un video
    //se sì lo assegna alla variabile currentPlayer e alla fine della riproduzione
    //attiverà la funzione AutoCloseHotspots
    public void CheckVideoStatus()
    {
        foreach (var i in VideoPlayers)
        {
            currentPlayer = i.GetComponent<VideoPlayer>();
            if (currentPlayer.isPlaying)
            {
                currentPlayer.loopPointReached += AutoCloseHotspots;
                uiController.MenuButton.enabled = false;
            }
        }
        MainPlayer.loopPointReached += AutoCloseButtons;
    }

    /*
    public void HotspotPlayer(VideoPlayer player)
    {
        if (player.isPrepared)
        {
            player.timeUpdateMode = VideoTimeUpdateMode.DSPTime;
            player.Play();
        }
    }
    */

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
            uiController.Pause();
            uiController.Blur(true);
            uiController.PauseButton.SetActive(true);
            uiController.MenuButton.enabled = true;
        }
        catch
        {
            throw new NotImplementedException();
        }
    }

    public void AutoCloseButtons(VideoPlayer player)
    {
        uiController.PauseButton.SetActive(false);
        uiController.MenuButton.gameObject.SetActive(false);
        uiController.RestartButton.SetActive(true);
        uiController.MenuCanvas.SetActive(false);
    }

}
