using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButton : MonoBehaviour
{
    [SerializeField]
    private Transform puzzleField;

    [SerializeField]
    private GameObject btn;

    // first function to be called when we run our script
    void Awake()
    {
        for(int i = 0; i < 8; i++) 
        {
            GameObject button = Instantiate(btn); //instantiate is a function of MonoBehaviour, it creates a copy of the game objec twe pass to it
            button.name = "" + i; //naming the buttons with numbers, from 0 up to i-1
            button.transform.SetParent(puzzleField, false);    
        }
    }
}
