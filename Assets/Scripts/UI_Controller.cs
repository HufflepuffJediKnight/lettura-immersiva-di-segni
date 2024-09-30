using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class UI_Controller : MonoBehaviour
{
    int currentScene;

    public bool IsPaused { get; set; }

    public static Animator AnimationController { get; private set; }
    public static Button Button1 { get; set; }
    public static Button Button2 { get; set; }
    public static GameObject PauseButton { get; set; }
    public static GameObject PlayButton { get; set; }
    public static GameObject RestartButton { get; set; }
    public static Button MenuButton { get; set; }
    public static GameObject MenuCanvas { get; set; }

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
