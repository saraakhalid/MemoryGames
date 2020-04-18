using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class test : MonoBehaviour
{
    [SerializeField] //so that the variable can appear in the editor
    public GameObject username; //for storing the inputField game object
    private string Username;

    [SerializeField]
    public GameObject onScreenGreeting;


    public void onLogin(){
        string message = string.Format("Hello, {0}", Username); 
        print(message);

        onScreenGreeting.GetComponent<Text>().text = message;
    }

    void Update()
    {
        Username = username.GetComponent<InputField>().text; //this will get whatever is written inside the inputfield and store it as string inside the Username variable
    }
}
