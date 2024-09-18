using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Menu_Controller : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(1);
    }
    public void SceneChooser(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

}
