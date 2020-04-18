using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Redirection : MonoBehaviour
{
    public void redirectToScreen(string sceneName)
    {
        if (SubmitButton.roundNumber == 4)
        {
            SceneManager.LoadScene(8); //build index of Congrats scene is 8
            SubmitButton.roundNumber++; //so that it does not enter this if statement again
        }
        else
            SceneManager.LoadScene(sceneName); //old: Application.LoadLevel(sceneName) but compiler said it's obsolete
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
