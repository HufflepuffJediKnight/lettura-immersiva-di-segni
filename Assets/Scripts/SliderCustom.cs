using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderCustom : MonoBehaviour
{

    [SerializeField] private Image handleImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image fillImage;

    void Start()
    {
        handleImage.canvasRenderer.SetAlpha(0.2f);
        backgroundImage.canvasRenderer.SetAlpha(0.2f);
        fillImage.canvasRenderer.SetAlpha(0.2f);
    }

   
}
