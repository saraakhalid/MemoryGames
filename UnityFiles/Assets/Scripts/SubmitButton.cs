using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class SubmitButton : MonoBehaviour
{ 
    public static int roundNumber = 1;
    string roundComponentsGroupName;
    // public ToggleGroup ToggleGroup;
    [SerializeField]
    public GameObject[] toggleGroup; //a toggle group of my creation

    List<string> clickedAudios = new List<string>(); //what the user clicked/chose
    private int Score;
    [SerializeField]
    private GameObject EndOfRound;

    [SerializeField]
    private Text txtEndOfRound = null;

    [SerializeField]
    private Image thumbsUp;
    [SerializeField]
    private Image thumbsDown;
    [SerializeField]
    AudioSource winSound;
    [SerializeField]
    private GameObject[] RoundComponents;

    void Awake(){

        Debug.Log("Good Morning"); //debug
        setLevelNumber();
        winSound = GetComponent<AudioSource>();

        //DontDestroyOnLoad(this);
        // Destroy(ToggleGroup);
        clickedAudios = new List<string>();

        toggleGroup = GameObject.FindGameObjectsWithTag("ToggleButton");
        
    }
    void Update(){
        toggleGroup = GameObject.FindGameObjectsWithTag("ToggleButton");
        //if( ToggleGroup == null ) ToggleGroup = GetComponent<ToggleGroup>(); //make sure I have toggle group
    }
    int levelNumber;
    [SerializeField]
    string level;
    private void setLevelNumber(){
        if(level == "Easy")
        {levelNumber = 1;}
        else if(level == "Med")
        {levelNumber = 2;}
        else if(level == "Hard")
        {levelNumber = 3;}

        Debug.Log("level number is: " + levelNumber);
    }

    int aims; //number of correct choices
    int miss; //number of wrong choices

    public void clickingSubmit(){
        Debug.Log("Hello from inside Submit");
        for(int i=0; i<levelNumber; i++)
        {
            if(clickedAudios[i].IndexOf('*') != -1) //if the char * exists in the name of a button, it is the correct choice button
            {
                Debug.Log("Choice is correct!");
                aims++;
            }
            else
            {
                Debug.Log(clickedAudios[i]);
                Debug.Log("Choice is wrong!");
                miss++;
            }
        }
        
        //displaying win/lose screen
        foreach(var rnd in RoundComponents){
            if (rnd.activeSelf)
                rnd.SetActive(false);
        }
        EndOfRound.SetActive(true);

        if(levelNumber == 1 && aims ==1)
        {
            userWins();
        }
        else if(levelNumber == 2 && aims == 2)
        {
            userWins();
        }
        else if(levelNumber == 3 && aims == 3)
        {
            userWins();
        }
        else
        {
            userLoses();
        }
    }
    private void userWins(){
        print("You win this round!");
        txtEndOfRound.text = "You win this round! :)";
        thumbsUp.enabled = true;
        thumbsDown.enabled = false;
        winSound.Play();
        Score = Score + 10;
        roundNumber++;
    }
    private void userLoses(){
        print("You lose this round!");
        txtEndOfRound.text = "You lose this round! :(";
        thumbsUp.enabled = false;
        thumbsDown.enabled = true;
        roundNumber++;
    }

    public void clickingToggle(){ //called every time a toggle is presseds

        GameObject activeToggle = EventSystem.current.currentSelectedGameObject;
        GameObject[] twoActiveToggles; //for Medium level
        GameObject[] threeActiveToggles; //for Hard level

        //1. activate only the clicked toggle and deactivate all other toggles in the screen
        if(levelNumber == 1){
        foreach(var tgl in toggleGroup)
        {
            if (tgl == activeToggle)
            {
                tgl.GetComponent<Toggle>().isOn = true;
            }
            else
                tgl.GetComponent<Toggle>().isOn = false;
        }
        }
        // else if(levelNumber == 2)
        // {
        //     activeToggles.Add(activeToggle);
        // }

        //2. get the name of the currently active toggle
        string selectedToggle = activeToggle.name;

        Debug.Log("active toggle: ");
        print(selectedToggle);

        //3. add it to an array of user answers for validation when submitting
        if(levelNumber == 1){
            clickedAudios.Clear(); //clear the array
            clickedAudios.Add(selectedToggle); //add the name of the toggle to the array
        }
        // else if(levelNumber == 2)
        // {
        //   //write code for what happens in level 2
        // }

        //debugging ....
        //displaying clickedAudios
        print("displaying clicked audios: ");
        for(int i=0; i<clickedAudios.Count;i++){
            Debug.Log(clickedAudios[i]);
        }
    }

    public void goToNextRound(){
        EndOfRound.SetActive(false);

        if(roundNumber==2)
        RoundComponents[1].SetActive(true); //activate the second round components
        else if(roundNumber==3)
        RoundComponents[2].SetActive(true);
    }
}