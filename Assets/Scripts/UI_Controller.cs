using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using System.Runtime.InteropServices.WindowsRuntime;
using System;

public class UI_Controller : MonoBehaviour
{
    /// <summary>
    /// La classe UI_Controller si occupa delle funzioni relative agli elementi di interfaccia grafica come: il menu a tendina,
    /// i due pulsanti per aprire gli hotspot, i pulsanti di pausa/play e quello di replay.
    /// </summary>

   
    [SerializeField] private Animator _animator;
    [SerializeField] private Button button1;
    [SerializeField] private Button button2;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject playButton;

    [SerializeField] private GameObject fullscreenButton;
    [SerializeField] private GameObject fullscreenExitButton;

    [SerializeField] private GameObject menuButton;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject menuPanel;

    [SerializeField] private GameObject hotspot1;
    [SerializeField] private GameObject hotspot2;


    private bool isPaused;


    private void OnEnable()
    {
        Video_Controller.OnMainVideoFinished += VideoController_OnMainVideoFinished;
    }

    
    private void OnDisable()
    {
        Video_Controller.OnMainVideoFinished -= VideoController_OnMainVideoFinished;
    }


    private void Start()
    {

        button1.onClick.AddListener(delegate { ButtonDestroyer(button1); });
        button2.onClick.AddListener(delegate { ButtonDestroyer(button2); });
    }

    // qui viene controllato se il dispositivo ha un touchscreen
    private void Update()
    {
        CheckInputMethod();
    }

    private void VideoController_OnMainVideoFinished()
    {
        EnableRestartButton();
    }

    private void EnableRestartButton()
    {
        restartButton.SetActive(true);

        pauseButton.SetActive(false);
        menuButton.SetActive(false);
        menuPanel.SetActive(false);
        fullscreenButton.SetActive(false);
        fullscreenExitButton.SetActive(false);
    }


    // queste funzioni vengono chiamate rispettivamente dai pulsanti Button_Fullscreen e Button_Fullscreen_Exit
    public void Fullscreen()
    {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
    }
    public void FullscreenExit()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
    }

    
    public void PauseMainVideo()
    {
//        pauseButton.SetActive(false);
//        playButton.SetActive(true);

        Time.timeScale = 0.0f;
        isPaused = true;     
    }

    public void PlayMainVideo()
    {
//        pauseButton.SetActive(true);
//        playButton.SetActive(false);

        Time.timeScale = 1.0f;
        isPaused = false;
    }

    // la funzione di restart è assegnata al pulsante Button_Restart che compare una volta terminato il video principale
    public void RestartScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentScene);
    }

    // questa è la funzione che si occupa di attivare il blur dello sfondo mentre è attivo uno dei due hotspot 
    public void Blur(bool isBlurred)
    {
        PostProcessVolume postVolume = Camera.main.gameObject.GetComponent<PostProcessVolume>();

        if (!isBlurred)
        {
            postVolume.enabled = true;
        }
        else
        {
            postVolume.enabled = false;
        }

    }

    /// <summary>
    /// questa funzione dovrebbe distruggere i pulsanti degli hotspot una volta cliccati in una versione precedente
    /// il pulsante veniva disattivato (non era cliccabile) ma rimaneva comunque sullo schermo penso che si possa
    /// risolvere chiamandaìo il colorblock come nella funzione CheckInputMethod presente sotto invece di .image.color
    /// </summary>
    /// <param name="button"></param>
    private void ButtonDestroyer(Button button)
    {
        Button.Destroy(button);
        _animator.enabled = false;
        button.image.color = new Color(0, 0, 0, 0);
        _animator.enabled = true;
    }

    /// <summary>
    /// questa funzione serve a rilevare se il dispositivo ha un touchscreen, se viene rilevato un tocco dello schermo
    /// il pulsante pausa si ingrandisce, per essere toccato più facilmente, e cambia colore per non essere trasparente
    /// come di default, è da cambiare così che ogni volta che viene toccato lo schermo il pulsante di pausa scompaia dopo un po' 
    /// </summary>
    /// 

    // TODO
    private void CheckInputMethod()
    {
        Button pauseButtonTest = pauseButton.GetComponent<Button>();
        if (Input.touchCount != 0)
        {
            ColorBlock cb = pauseButtonTest.colors;
            cb.normalColor = new Color(250, 250, 250);
            pauseButtonTest.colors = cb;
            pauseButton.transform.localScale = new Vector3(2, 2, 2);
            pauseButton.transform.position = new Vector3(0, -440, 0);
            playButton.transform.localScale = new Vector3(2, 2, 2);
            playButton.transform.position = new Vector3(0, -440, 0);
        }
    }

    public bool IsVideoPaused()
    {
        return isPaused;
    }
   
}
