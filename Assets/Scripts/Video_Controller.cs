using UnityEngine;
using UnityEngine.Video;
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Events;

public class Video_Controller : MonoBehaviour
{
    // la classe Video_Controller si occupa della gestione dei VideoPlayer e delle funzioni che
    // vengono chiamate una volta terminati

    public static UnityAction OnMainVideoFinished;

    [SerializeField] private Button menuButton; 
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject restartButton; 
    [SerializeField] private GameObject menuCanvas;

    [SerializeField] private VideoPlayer _mainVideoPlayer;

    // qui vengono dichiarate le proprietà relative ai player video
    [SerializeField] private VideoPlayer[] videoPlayersArray;
    [SerializeField] GameObject[] hotspostsArray;

    private VideoPlayer currentPlayer;
    private UI_Controller uiController = new UI_Controller();


    private void Start()
    {
        PrepareVideos();
        
    }

    private void Update()
    {
        CheckVideoStatus();
    }


    /// <summary>
    /// controlla se uno dei due videoplayer degli hotspot sta riproducendo un video
    /// se sì lo assegna alla variabile currentPlayer e alla fine della riproduzione
    /// attiverà la funzione AutoCloseHotspots. Invece alla fine del video principale
    /// verrà attivata la funzione AutoCloseButtons
    /// </summary>
    private void CheckVideoStatus()
    {
        foreach (var vp in videoPlayersArray)
        {
            currentPlayer = vp.GetComponent<VideoPlayer>();

            if (currentPlayer.isPlaying)
            {
                currentPlayer.loopPointReached += AutoCloseHotspots;
//                menuButton.enabled = false;
            }
        }

        _mainVideoPlayer.loopPointReached += FinishMainVideo;
    }

    // questi due metodi commentati dovevano servire a risolvere il problema dello stutter all'inizio
    // degli hotspot, preparando i video e facendoli partire solo una volta preparati, andando a sostituire
    // con HotspotPlayer il metodo Play del componente VideoPlayer di Unity

    
    private void PrepareVideos()
    {
        foreach(var vp in videoPlayersArray)
        {
            vp.GetComponent<VideoPlayer>().Prepare();
        }     
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

    /// <summary>
    /// disattiva entrambi gli hotspots (anche se uno dei due sarà già disattivo) che prende dall' array Hotspots,
    /// disattiva il blur, e riattiva i pulsanti della UI
    /// </summary>
    /// <param name="player"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void AutoCloseHotspots(VideoPlayer player)
    {
        try
        {
            foreach (var hotspot in hotspostsArray)
            {
                hotspot.SetActive(false);
            }
            uiController.PlayMainVideo();
            uiController.Blur(true);
            pauseButton.SetActive(true);
            menuButton.enabled = true;
        }
        catch
        {
            throw new NotImplementedException();
        }
    }

    private void FinishMainVideo(VideoPlayer player)
    {
        OnMainVideoFinished?.Invoke();      
    }
    

}
