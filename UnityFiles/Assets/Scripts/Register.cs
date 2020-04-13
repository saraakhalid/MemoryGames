using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class Register : MonoBehaviour
{
    public GameObject username;
    public GameObject password;
    public GameObject confPassword;
    private string _Username;
    private string _Password;
    private string ConfPassword;
    [SerializeField]
    public GameObject errorPanel;
    [SerializeField]
    public GameObject RegisterSuccessPanel;
    private string form; //holds all string variables
    //sStart is called before the first frame update
    void Start()
    {
        //logging in with user preferences
        // if(PlayerPrefs.HasKey("USERNAME")){
        //     _Username = PlayerPrefs.GetString("USERNAME");
        //     _Password = PlayerPrefs.GetString("PASSWORD");
        //     var request = new LoginWithEmailAddressRequest {Username = _Username, Password=_Password};
        //     PlayFabClientAPI.LoginWithEmailAddressRequest(request, OnLoginSuccess, OnLoginFail);
        //      PlayFabClientAPI.LoginWithPlayfab(??)
        // }
    }

    public void RegisterButton(){

        // private bool UsernameValid = false;
        // private bool PasswordValid = false;
        // private bool ConfPasswordValid = false;
        bool UsernameValid = false;
        bool PasswordValid = false;
        bool ConfPasswordValid = false;

        if (_Username != ""){
            if(!System.IO.File.Exists(@"D:/GAMES/Puzzle Game/Usernames/"+_Username+".txt")){ //check if file with Username exists
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

        if(_Password != ""){
                if(_Password.Length > 16)
                {
                    Debug.LogWarning("password too long! Password must be less than 16 characters.");
                }
                else if(_Password.Length < 5)
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
            if (ConfPassword == _Password)
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
            form = (_Username+"\n"+_Password);
            System.IO.File.WriteAllText(@"D:/GAMES/Puzzle Game/Usernames/"+_Username+".txt", form); //form is the contents of the file
            //3. clear all fields
            _Username = username.GetComponent<InputField>().text = "";
            _Password = password.GetComponent<InputField>().text = "";
            ConfPassword = confPassword.GetComponent<InputField>().text = "";
            //4. print message
            print ("Registration complete!");
        }
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result){
        Debug.Log("Registration successful!");
        string message = "Registration successful!";
        RegisterSuccessPanel.transform.GetChild(0).GetComponent<Text>().text = "You have successfully registered an account!";
        RegisterSuccessPanel.SetActive(true);
        StartCoroutine(RemoveAfterSeconds(3, RegisterSuccessPanel));
        //save username and password in user preferences so that user will be logged in next time
        PlayerPrefs.SetString("USERNAME", _Username);
        PlayerPrefs.SetString("PASSWORD", _Password);
    }
    private void OnRegisterFail(PlayFabError error){
        Debug.LogError(error.GenerateErrorReport());
    }
    public void RegisterWithPlayFab(){
        var registerRequest = new RegisterPlayFabUserRequest { Username = _Username, Password = _Password, RequireBothUsernameAndEmail=false};
        //note: Password must be 6 and 100 characters
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest{
            Username = _Username, 
            Password = _Password, 
            RequireBothUsernameAndEmail=false
        }, (result) => 
        {
           OnRegisterSuccess(result);
        }, (error) => 
        {
            handleRegistrationErrors(error);
        });

    }

    //for handling registration errors
    private void handleRegistrationErrors(PlayFabError e)
    {
        //1. get the error message - refer to https://community.playfab.com/questions/983/211976307-Error-codes-on-unity.html
        string message = string.Empty;
        switch (e.Error)
	        {
                case PlayFabErrorCode.InvalidParams:
                    message = "Invalid Parameters. Please contact support!";
                break;
        
                case PlayFabErrorCode.NotAuthenticated:
                    message = "Player not authorized/authenticated";
                    break;
        
                case PlayFabErrorCode.NameNotAvailable:
                case PlayFabErrorCode.UsernameNotAvailable:
                    message = "Username or Displayname is already taken/not available";
                    break;
        
                default:
                    Debug.LogError(e.Error);
                    Debug.LogError(e.ErrorMessage);
                    break;
            }
        // //2. show error message on the screen
        GameObject errorText = errorPanel.transform.GetChild(0).gameObject;
        errorText.GetComponent<Text>().text = message;
        errorPanel.SetActive(true);
        StartCoroutine(RemoveAfterSeconds(5, errorPanel));
    }

    IEnumerator RemoveAfterSeconds(int seconds, GameObject obj)
    {
            yield return new WaitForSeconds(seconds);
            obj.SetActive(false);
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
            if(_Username != "" && _Password != "" && ConfPassword!="") //note: we can't use null in the below case, because even though it appears empty but it isn't null
            {
                RegisterButton();
            }
            else
            {
                print("please fill up the fields first.");
            }
        }
       _Username = username.GetComponent<InputField>().text;
       _Password = password.GetComponent<InputField>().text;
       ConfPassword = confPassword.GetComponent<InputField>().text;
    }
}

/* 
References:
- https://www.youtube.com/watch?v=KH6mLZxgvgk&list=PLWeGoBm1YHVgi6ZcwWGt27Y4NHUAG5smX&index=2
*/