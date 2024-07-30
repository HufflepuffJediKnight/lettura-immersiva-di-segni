using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class UI_Controller : MonoBehaviour
{
    public VideoPlayer[] videoPlayers;
    void PrepareVideos()
    {
        for (int i = 0; i < videoPlayers.Length; i++) GetComponent<VideoPlayer>().Prepare();
    }
/*
    public void Fullscreen()
    {
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
    }

    public void FullscreenExit()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
    }
*/
    private void Start()
    {
        PrepareVideos();
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
    }

    public void Play()
    {
        Time.timeScale = 1.0f;
    }
/*
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
*/
}
