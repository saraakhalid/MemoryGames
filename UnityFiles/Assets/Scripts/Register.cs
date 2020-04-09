using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    public GameObject username;
    public GameObject password;
    public GameObject confPassword;
    private string Username;
    private string Password;
    private string ConfPassword;
    private string form; //holds all string variables
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    public void RegisterButton(){

        // private bool UsernameValid = false;
        // private bool PasswordValid = false;
        // private bool ConfPasswordValid = false;
        bool UsernameValid = false;
        bool PasswordValid = false;
        bool ConfPasswordValid = false;

        if (Username != ""){
            if(!System.IO.File.Exists(@"D:/GAMES/Puzzle Game/Usernames/"+Username+".txt")){ //check if file with username exists
                UsernameValid = true; //it passed the tests, it is valid
            }   
            else
            {
                Debug.LogWarning("Username exists. Choose another."); 
            }
        }
        else{
            Debug.LogWarning("Username is empty");
        }

        if(Password != ""){
                if(Password.Length > 16)
                {
                    Debug.LogWarning("password too long! Password must be less than 16 characters.");
                }
                else if(Password.Length < 5)
                {
                    Debug.LogWarning("password too short! Password must be more than 4 characters.");
                }
                else
                    PasswordValid = true;
        }
        else
            Debug.LogWarning("Dude, is the password field empty?");
        
        if(ConfPassword != "")
        {
            if (ConfPassword == Password)
                {
                    ConfPasswordValid = true;
                }
            else
                Debug.LogWarning("passwords do not match!");
        }
        else
            Debug.LogWarning("Confirm Password field is empty.");
        
        if(UsernameValid && PasswordValid && ConfPasswordValid)
        {
            //1. encrypt the password


            //2. store the info
            form = (Username+"\n"+Password);
            System.IO.File.WriteAllText(@"D:/GAMES/Puzzle Game/Usernames/"+Username+".txt", form); //form is the contents of the file
            //3. clear all fields
            Username = username.GetComponent<InputField>().text = "";
            Password = password.GetComponent<InputField>().text = "";
            ConfPassword = confPassword.GetComponent<InputField>().text = "";
            //4. print message
            print ("Registration complete!");
        }
    }
    // Update is called once per frame
    void Update()
    {
        //for my debugging use - useless in mobile 
        if(Input.GetKeyDown(KeyCode.Tab)){
            if(username.GetComponent<InputField>().isFocused){
                password.GetComponent<InputField>().Select();
            }
            if(password.GetComponent<InputField>().isFocused){
                confPassword.GetComponent<InputField>().Select();
            }
        }
        if(Input.GetKeyDown(KeyCode.Return)){
            //if enter is clicked, validate that all fields are full
            if(Username != "" && Password != "" && ConfPassword!="") //note: we can't use null in the below case, because even though it appears empty but it isn't null
            {
                RegisterButton();
            }
            else
            {
                print("please fill up the fields first.");
            }
        }
       Username = username.GetComponent<InputField>().text;
       Password = password.GetComponent<InputField>().text;
       ConfPassword = confPassword.GetComponent<InputField>().text;
    }
}
