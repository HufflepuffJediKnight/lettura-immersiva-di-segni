using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Controller : MonoBehaviour
{
    // questo script serve solo a scegliere una delle tre scene (multipiattaforma, vr e cardboard) dai pulsanti del menu

    public static void SceneChooser(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
