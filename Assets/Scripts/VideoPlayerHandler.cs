using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerHandler : MonoBehaviour
{
    [SerializeField] private Slider _timeSlider;
    [SerializeField] private Animator _animator;

    private VideoPlayer _videoPlayer;
    private bool _isDragging = false;

    private void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
//        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _timeSlider.onValueChanged.AddListener(HandleTimeSliderValueChanged);
    }

    private void Update()
    {
        if (!_isDragging && _videoPlayer)
        {
            _timeSlider.value = (float)_videoPlayer.time;
            

            if (_timeSlider.maxValue != (float)_videoPlayer.length)
            {
                _timeSlider.maxValue = (float)_videoPlayer.length;
            }
        }
    }


    public void BeginDrag()
    {
        _isDragging = true;
    }

    public void EndDrag()
    {
        _isDragging = false;
    }

    public void HandleTimeSliderValueChanged(float value)
    {
        if (_isDragging && _videoPlayer)
        {
            _videoPlayer.time = value;
//            _animator.Play("Base Layer.UI_Animation", -1, value);
        }
    }
}
