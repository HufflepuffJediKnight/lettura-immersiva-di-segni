using UnityEngine;
using UnityEngine.Video;
using System;
using System.Linq;
using UnityEngine.UI;

public class Video_Controller : MonoBehaviour
{
    static UI_Controller uiController;
    static Button menuButton = UI_Controller.MenuButton;
    static GameObject pauseButton = UI_Controller.PauseButton;
    static GameObject restartButton = UI_Controller.RestartButton;
    static GameObject menuCanvas = UI_Controller.MenuCanvas;
    VideoPlayer currentPlayer;

    public  static GameObject[] VideoPlayers
    {
        get { return VideoPlayers; }
    }
    public static VideoPlayer MainPlayer
    {
        get { return MainPlayer;}
        set { }
    }
    public static GameObject[] Hotspots
    {
        get { return Hotspots; }
        set { }
    }

    private void Start()
    {
        VideoPlayers.Append(MainPlayer.gameObject).ToArray();
        VideoPlayers.Append(GameObject.Find("Hotspot_1_Player")).ToArray();
        VideoPlayers.Append(GameObject.Find("Hotspot_2_Player")).ToArray();

        MainPlayer = GameObject.Find("Video_Player").GetComponent<VideoPlayer>();

        Hotspots.Append(GameObject.Find("Hotspot_1")).ToArray();
        Hotspots.Append(GameObject.Find("Hotspot_2")).ToArray();
    }

    private void Update()
    {
//        PrepareVideos();
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
                menuButton.enabled = false;
            }
        }
        MainPlayer.loopPointReached += AutoCloseButtons;
    }
/*
    public void PrepareVideos()
    {
        for (int i = 0; i < VideoPlayers.Length; i++)
        {
            VideoPlayers[i].GetComponent<VideoPlayer>().Prepare();
        }
        MainPlayer.GetComponent<VideoPlayer>().Prepare();
    }
*/
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
