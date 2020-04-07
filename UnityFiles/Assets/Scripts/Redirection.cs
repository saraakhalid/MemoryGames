using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redirection : MonoBehaviour
{
    public void redirectToScreen(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }
    public void QuitGame(){
        Application.Quit();
    }
}
