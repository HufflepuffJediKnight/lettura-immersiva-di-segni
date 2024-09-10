using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Menu_Controller : MonoBehaviour
{

    public void SceneChooser(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

}
