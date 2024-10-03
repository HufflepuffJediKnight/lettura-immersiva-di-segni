using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Input_Controller : MonoBehaviour
{
    /// <summary>
    /// questa classe serve a gestire la pausa tramite barra spaziatrice, in cui vengono chiamati metodi e propriet�
    /// da UI_Controller e Video_Controller
    /// </summary>

   
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject playButton; 
    [SerializeField] private GameObject[] hotspotsArray;


    private UI_Controller uiController;


    private void Awake()
    {
        uiController = FindObjectOfType<UI_Controller>();
    }

    void Update()
    {
        PauseToggle();
    }

    /// <summary>
    /// qui viene controllato se viene premuta la barra spaziatrice, dopodich� viene controllato 
    /// tramite la variabile IsPaused se l'applicazione � in pausa o meno, vengono chiamate le
    /// corrispettive funzioni di pausa/play e, in base al caso vengono attivati e disattivati
    /// i pulsanti Button_Pause e ButtonPlay. infine se uno hotspot � attivo non "dovrebbe"
    /// fare niente, per� va cambiato in modo da gestire la pausa/play degli hotspot
    /// </summary>
    public void PauseToggle()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!uiController.IsVideoPaused())
            {
                uiController.PauseMainVideo();
                pauseButton.SetActive(false);
                playButton.SetActive(true);
            }

            else if (uiController.IsVideoPaused())
            {
                uiController.PlayMainVideo();
                pauseButton.SetActive(true);
                playButton.SetActive(false);
            }
            else
            {
                foreach (var i in hotspotsArray)
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
