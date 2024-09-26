using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class UI_Controller : MonoBehaviour
{
    int currentScene;
    [SerializeField] bool _isPaused;
    Animator _animationController;
     Button _button1;
    Button _button2;
    GameObject _pauseButton;
    GameObject _playButton;
    Button _menuButton;
    GameObject _menuCanvas;
    GameObject _restartButton;

    public bool IsPaused { get { return _isPaused; } set { _isPaused = value; } }
    public Animator AnimationController
    {
        get { return _animationController; }
        set { _animationController = value; }
    }
    public Button Button1
    {
        get { return _button1; }
        set { _button1 = value; }
    }
    public Button Button2
    {
        get { return _button2; }
        set { _button2 = value; }
    }
    public GameObject PauseButton
    {
        get { return _pauseButton; }
        set { _pauseButton = value; }
    }
    public GameObject PlayButton
    {
        get { return _playButton; }
        set { _playButton = value; }
    }
    public GameObject RestartButton
    {
        get { return _restartButton; }
        set { _restartButton = value; }
    }
    public Button MenuButton
    {
        get { return _menuButton; }
        set { _menuButton = value; }
    }
    public GameObject MenuCanvas
    {
        get { return _menuCanvas; }
        set { _menuCanvas = value; }
    }

    public void Fullscreen()
    {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
    }
    public void FullscreenExit()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
    }


    private void Start()
    {
        IsPaused = false;
        _animationController = GetComponent<Animator>();
        _button1 = GameObject.Find("Button_1").GetComponent<Button>();
        _button2 = GameObject.Find("Button_2").GetComponent<Button>();
        _pauseButton = GameObject.Find("Button_Pause");
        _playButton = GameObject.Find("Button_Play");
        _restartButton = GameObject.Find("Button_Restart");
        _menuButton = GameObject.Find("Button_Menu").GetComponent<Button>();
        _menuCanvas = GameObject.Find("Menu_Canvas");

        currentScene = SceneManager.GetActiveScene().buildIndex;

        Button1.onClick.AddListener(delegate { ButtonDestroyer(Button1); });
        Button2.onClick.AddListener(delegate { ButtonDestroyer(Button2); });
    }

    private void Update()
    {
        CheckInputMethod();
    }

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

    public void Restart()
    {
        SceneManager.LoadScene(currentScene);
    }

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

    public void ButtonDestroyer(Button button)
    {
        Button.Destroy(button);
        AnimationController.enabled = false;
        button.image.color = new Color(0, 0, 0, 0);
        AnimationController.enabled = true;
    }

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
