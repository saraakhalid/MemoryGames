using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ListenAgain : MonoBehaviour
{
    public static int timesListened = 0; //I made it static so that it keeps its value after scene changes
    public int timesAllowed;
    public GameObject triesLeft;

    void Start(){
        timesListened = PlayerPrefs.GetInt("times listened");
        triesLeft.GetComponent<Text>().text = (timesAllowed - timesListened).ToString();
    }
    void Update(){
        PlayerPrefs.SetInt("times listened", timesListened);
    }
    public void onClickingListenAgain(){
        if(timesListened == timesAllowed)
            print("You have used up all of your chances!");
        else{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); //load previous scene
        timesListened++;
        triesLeft.GetComponent<Text>().text = (timesAllowed - timesListened).ToString();
        }
    }
}


/*
References:
- player prefs: https://answers.unity.com/questions/1325056/how-to-use-playerprefs-2.html
 */