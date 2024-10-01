using UnityEngine;
using UnityEngine.Video;
using System;
using System.Linq;
using UnityEngine.UI;

public class Video_Controller : MonoBehaviour
{
    // la classe Video_Controller si occupa della gestione dei VideoPlayer e delle funzioni che
    // vengono chiamate una volta terminati

    /// <summary>
    /// qui viene istanziata la classe UI_Controller e vengono dichiarate delle variabili
    /// con i valori provenienti dallo script UI_Controller
    /// </summary>
    UI_Controller uiController;
    Button menuButton = UI_Controller.MenuButton;
    GameObject pauseButton = UI_Controller.PauseButton;
    GameObject restartButton = UI_Controller.RestartButton;
    GameObject menuCanvas = UI_Controller.MenuCanvas;
    VideoPlayer currentPlayer;

    // qui vengono dichiarate le proprietà relative ai player video
    public  static GameObject[] VideoPlayers 
    {
        // array dei GameObject che contengono il componente VideoPlayer, ovvero quello del video principale e i due hotspot
        get { return VideoPlayers; }
        set { }
    }
    public static VideoPlayer MainPlayer
    {
        // che contiene il player del video principale
        get { return MainPlayer;}
        set { }
    }
    public static GameObject[] Hotspots
    {
        // array dei componenti Videoplayer dei due Hotspot, è praticamente un duplicato dell' array precedente che
        // serve per il metodo AutoCloseHotspots e il PauseToggle in Input_Controller
        get { return Hotspots; }
        set { }
    }

    /// <summary>
    /// qui vengono inizializzati gli array di VideoPlayer e GameObject e la proprietà MainPlayer
    /// </summary>
    private void Start()
    {
        MainPlayer = GameObject.Find("Video_Player").GetComponent<VideoPlayer>();
        VideoPlayers.Append(MainPlayer.gameObject).ToArray();
        VideoPlayers.Append(GameObject.Find("Hotspot_1_Player")).ToArray();
        VideoPlayers.Append(GameObject.Find("Hotspot_2_Player")).ToArray();

        Hotspots.Append(GameObject.Find("Hotspot_1")).ToArray();
        Hotspots.Append(GameObject.Find("Hotspot_2")).ToArray();
    }

    /// <summary>
    /// viene chiamata la funzione CheckVideoStatus e ho provato a chiamare PrepareVideos in Update
    /// invece che in Start,però in entrambi casi non sembrava avere alcun effetto
    /// </summary>
    private void Update()
    {
//        PrepareVideos();
        CheckVideoStatus();
    }

    /// <summary>
    /// controlla se uno dei due videoplayer degli hotspot sta riproducendo un video
    /// se sì lo assegna alla variabile currentPlayer e alla fine della riproduzione
    /// attiverà la funzione AutoCloseHotspots. Invece alla fine del video principale
    /// verrà attivata la funzione AutoCloseButtons
    /// </summary>
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

    // questi due metodi commentati dovevano servire a risolvere il problema dello stutter all'inizio
    // degli hotspot, preparando i video e facendoli partire solo una volta preparati, andando a sostituire
    // con HotspotPlayer il metodo Play del componente VideoPlayer di Unity

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

    // serve a disattivare il pulsante di pausa, il pulsante del menu e il menu (se attivo) e attivare il pulsante replay alla fine del video
    public void AutoCloseButtons(VideoPlayer player)
    {
        pauseButton.SetActive(false);
        menuButton.gameObject.SetActive(false);
        restartButton.SetActive(true);
        menuCanvas.SetActive(false);
    }

}
