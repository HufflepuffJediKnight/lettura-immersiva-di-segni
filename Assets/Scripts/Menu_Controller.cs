using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Menu_Controller : MonoBehaviour
{
    public int nonvrindex;
    public int vrindex;

    public void SceneChooser(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

}
