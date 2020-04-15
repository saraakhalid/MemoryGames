using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System.Text.RegularExpressions;
public class Login : MonoBehaviour
{
    static public bool isLoggedIn;
    public GameObject username;
    public GameObject password;
    private string _Username;
    private string _Password;
    [SerializeField]
    public GameObject errorPanel;
    [SerializeField]
    public GameObject SuccessPanel;
    private float timeElapsed;
    private float delayBeforeLoading;

    void Start()
    {
        isLoggedIn = false;
        delayBeforeLoading = 10;
    }
    public void LoginWithPlayfab()
    {
        var registerRequest = new LoginWithPlayFabRequest { Username = _Username, Password = _Password };
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest
        {
            Username = _Username,
            Password = _Password
        }, (result) =>
        {
            onLoginSuccess(result);
        }, (error) =>
        {
            onLoginFail(error);
        });
    }

    private void onLoginSuccess(LoginResult result)
    {
        //print("Login success!");
        string message = string.Format("Hey there, {0}!", _Username);
        SuccessPanel.transform.GetChild(0).GetComponent<Text>().text = message;
        SuccessPanel.SetActive(true);
        isLoggedIn = true;
        StartCoroutine(Register.RemoveAfterSeconds(3, SuccessPanel));
        // StartCoroutine(WaitAWhile());
        if (!SuccessPanel.activeSelf && isLoggedIn)
            loadNextScene();

    }

    private void loadNextScene()
    {
        timeElapsed += 1;

        if (timeElapsed > delayBeforeLoading)
        {
            SceneManager.LoadScene("Start Scene");
        }
        else
            loadNextScene(); //keep looping until next scene is loaded
    }
    private void onLoginFail(PlayFabError e)
    {
        string errorMsg;
        // General purpose logging: GenerateErrorReport gives a bunch of information about the error
        Debug.Log(e.GenerateErrorReport());

        // Recognize and handle the error
        switch (e.Error)
        {
            case PlayFabErrorCode.InvalidTitleId:
                errorMsg = "Invalid title ID!";
                break;
            case PlayFabErrorCode.AccountNotFound:
                errorMsg = "Account was not found!";
                break;
            case PlayFabErrorCode.InvalidEmailOrPassword:
                errorMsg = "Invalid email or password!";
                break;
            case PlayFabErrorCode.RequestViewConstraintParamsNotAllowed:
                errorMsg = "Not allowed view parameters.."; //dunno what that means, tho ..
                break;
            //invalid input username - it must be between 3 to 20 characters
            case PlayFabErrorCode.InvalidUsernameOrPassword:
                errorMsg = "Invalid username or password (probably password)"; //why probably password? Because when username is invalid, it gives the "Account not found" error :)
                break;
            default:
                errorMsg = "An undefined error .. "; //a note for the developer to debug.log the error when this comes up
                break;
        }
        errorPanel.transform.GetChild(0).GetComponent<Text>().text = errorMsg;
        errorPanel.SetActive(true);
        StartCoroutine(Register.RemoveAfterSeconds(3, errorPanel));
    }

    void Update()
    {

        //for my debugging use - useless in mobile 
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //if enter is clicked, validate that all fields are full
            if (_Username != "" && _Password != "") //note: we can't use null in the below case, because even though it appears empty but it isn't null
            {
                LoginWithPlayfab();
            }
            else
            {
                print("please write something first ..");
            }
        }
        //initialising string variables here
        _Username = username.GetComponent<InputField>().text;
        _Password = password.GetComponent<InputField>().text;

        //display next scene after logging in when the user greeting disappears
        if (!SuccessPanel.activeSelf && isLoggedIn)
            loadNextScene();
    }
}

/* 
References:
- https://www.youtube.com/watch?v=KH6mLZxgvgk&list=PLWeGoBm1YHVgi6ZcwWGt27Y4NHUAG5smX&index=2
- playfab login error handling (official docs): https://docs.microsoft.com/en-us/gaming/playfab/features/automation/cloudscript/sdk-error-handling-best-practices
*/
