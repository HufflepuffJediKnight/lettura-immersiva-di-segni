using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Input_Controller : MonoBehaviour
{
    /// <summary>
    /// questa classe serve a gestire la pausa tramite barra spaziatrice, in cui vengono chiamati metodi e proprietà
    /// da UI_Controller e Video_Controller
    /// </summary>

    UI_Controller uiController;
    Video_Controller videoController;

    // qui vengono dichiarate le variabili necessarie assegnando i valori dallo script UI_Controller
    static GameObject pauseButton = UI_Controller.PauseButton;
    static GameObject playButton = UI_Controller.PauseButton;
    static GameObject[] hotspots;

    /// <summary>
    /// qui vengono assegnati all'array hotspots gli elementi dell'array Hotspots della classe Video_Controller
    /// l'ho commentato perché fa crashare l'applicazione 
    /// </summary>
    private void Start()
    {
//        hotspots = Video_Controller.Hotspots;
    }


    void Update()
    {
        PauseToggle();
    }

    /// <summary>
    /// qui viene controllato se viene premuta la barra spaziatrice, dopodiché viene controllato 
    /// tramite la variabile IsPaused se l'applicazione è in pausa o meno, vengono chiamate le
    /// corrispettive funzioni di pausa/play e, in base al caso vengono attivati e disattivati
    /// i pulsanti Button_Pause e ButtonPlay. infine se uno hotspot è attivo non "dovrebbe"
    /// fare niente, però va cambiato in modo da gestire la pausa/play degli hotspot
    /// </summary>
    public void PauseToggle()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!uiController.IsPaused)
            {
                uiController.Pause();
                pauseButton.SetActive(false);
                playButton.SetActive(true);
            }

            else if (uiController.IsPaused)
            {
                uiController.Play();
                pauseButton.SetActive(false);
                playButton.SetActive(true);
            }
            else
            {
                foreach (var i in hotspots)
                {
                    if (i.activeSelf)
                    {
                        return;
                    }
                }
            }
        }
    }
}
