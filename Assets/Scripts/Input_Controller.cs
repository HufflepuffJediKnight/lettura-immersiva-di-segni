using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Input_Controller : MonoBehaviour
{
    UI_Controller uiController;
    Video_Controller videoController;

    static GameObject pauseButton = UI_Controller.PauseButton;
    static GameObject playButton = UI_Controller.PauseButton;
    static GameObject[] hotspots;

    private void Start()
    {
//        hotspots = Video_Controller.Hotspots;
    }

    void Update()
    {
        PauseToggle();
    }

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
                foreach (var coso in hotspots)
                {
                    if (coso.activeSelf)
                    {
                        return;
                    }
                }
            }
        }
    }
}
