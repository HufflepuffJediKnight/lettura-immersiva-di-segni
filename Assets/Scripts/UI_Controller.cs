using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class UI_Controller : MonoBehaviour
{
    /// <summary>
    /// La classe UI_Controller si occupa delle funzioni relative agli elementi di interfaccia grafica come: il menu a tendina,
    /// i due pulsanti per aprire gli hotspot, i pulsanti di pausa/play e quello di replay.
    /// </summary>

    int currentScene;

    // qui vengono dichiarate le proprietà necessarie per gli script, ovvero gli elementi della UI come pulsanti e ui canvas
    
    public bool IsPaused { get; set; }

    private Animator AnimationController { get; set; }
    private Button Button1 { get; set; }
    private Button Button2 { get; set; }
    private GameObject PauseButton { get; set; }
    private GameObject PlayButton { get; set; }
    private GameObject RestartButton { get; set; }
    private Button MenuButton { get; set; }
    private GameObject MenuCanvas { get; set; }

    // queste funzioni vengono chiamate rispettivamente dai pulsanti Button_Fullscreen e Button_Fullscreen_Exit
    public void Fullscreen()
    {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
    }
    public void FullscreenExit()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
    }

    /// <summary>
    /// nello Start vengono assegnati i GameObject alle proprietà, viene assegnato l'indice di build della scena
    /// alla variabile currentScene che serve per il replay e vengono aggiunti due listener che dovrebbero disattivare
    /// permanentemente i due pulsanti degli hotspot una volta cliccati
    /// </summary>
    private void Start()
    {
        AnimationController = GetComponent<Animator>();
        Button1 = GameObject.Find("Button_1").GetComponent<Button>();
        Button2 = GameObject.Find("Button_2").GetComponent<Button>();
        PauseButton = GameObject.Find("Button_Pause");
        PlayButton = GameObject.Find("Button_Play");
        RestartButton = GameObject.Find("Button_Restart");
        MenuButton = GameObject.Find("Button_Menu").GetComponent<Button>();
        MenuCanvas = GameObject.Find("Menu_Canvas");

        currentScene = SceneManager.GetActiveScene().buildIndex;

        Button1.onClick.AddListener(delegate { ButtonDestroyer(Button1); });
        Button2.onClick.AddListener(delegate { ButtonDestroyer(Button2); });
    }

    // qui viene controllato se il dispositivo ha un touchscreen
    private void Update()
    {
        CheckInputMethod();
    }

    /// <summary>
    /// queste sono le funzioni per la gestione della pausa del video principale e dell'animazione che fa
    /// comparire i pulsanti hotspot, il resto dell'interfaccia grafica è animata in unscaled time
    /// che permette di interagirci anche durante la pausa. viene usata la proprietà booleana IsPaused
    /// per determinare all'interno della funzione PauseToggle in Input_Controller lo stato dell'applicazione
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0.0f;
        IsPaused = true;
    }

    public void Play()
    {
        Time.timeScale = 1.0f;
        IsPaused = false;
    }
    // la funzione di restart è assegnata al pulsante Button_Restart che compare una volta terminato il video principale
    public void Restart()
    {
        SceneManager.LoadScene(currentScene);
    }

    // questa è la funzione che si occupa di attivare il blur dello sfondo mentre è attivo uno dei due hotspot 
    public void Blur(bool isBlurred)
    {
        PostProcessVolume postVolume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
        if (!isBlurred)
        {
            postVolume.enabled = true;
            isBlurred = true;
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
    public void ButtonDestroyer(Button button)
    {
        Button.Destroy(button);
        AnimationController.enabled = false;
        button.image.color = new Color(0, 0, 0, 0);
        AnimationController.enabled = true;
    }

    /// <summary>
    /// questa funzione serve a rilevare se il dispositivo ha un touchscreen, se viene rilevato un tocco dello schermo
    /// il pulsante pausa si ingrandisce, per essere toccato più facilmente, e cambia colore per non essere trasparente
    /// come di default, è da cambiare così che ogni volta che viene toccato lo schermo il pulsante di pausa scompaia dopo un po' 
    /// </summary>
    public void CheckInputMethod()
    {
        Button pauseButton = PauseButton.GetComponent<Button>();
        if (Input.touchCount != 0)
        {
            ColorBlock cb = pauseButton.colors;
            cb.normalColor = new Color(250, 250, 250);
            pauseButton.colors = cb;
            pauseButton.transform.localScale = new Vector3(2, 2, 2);
            pauseButton.transform.position = new Vector3(0, -440, 0);
            PlayButton.transform.localScale = new Vector3(2, 2, 2);
            PlayButton.transform.position = new Vector3(0, -440, 0);
        }
    }
}
