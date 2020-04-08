using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Redirection : MonoBehaviour
{
    public void redirectToScreen(string sceneName)
    {
        SceneManager.LoadScene(sceneName); //old: Application.LoadLevel(sceneName) but compiler said it's obsolete
    }
    public void QuitGame(){
        Application.Quit();
    }
}
