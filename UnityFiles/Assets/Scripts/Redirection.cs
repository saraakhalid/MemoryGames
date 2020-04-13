using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Redirection : MonoBehaviour
{
    public void redirectToScreen(string sceneName)
    {
        if (SubmitButton.roundNumber == 4)
        {
            SceneManager.LoadScene(8); //build index of Congrats scene is 8
        }
        else
            SceneManager.LoadScene(sceneName); //old: Application.LoadLevel(sceneName) but compiler said it's obsolete
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
