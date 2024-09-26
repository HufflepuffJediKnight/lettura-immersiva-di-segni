using UnityEngine;

public class Input_Controller : MonoBehaviour
{
    UI_Controller uiController;
    Video_Controller videoController;

    private void Start()
    {
        
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
                uiController.PauseButton.SetActive(false);
                uiController.PlayButton.SetActive(true);
            }

            else if (uiController.IsPaused)
            {
                uiController.Play();
                uiController.PauseButton.SetActive(false);
                uiController.PlayButton.SetActive(true);
            }
            else
            {
                foreach (var coso in videoController.Hotspots)
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
