using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class SubmitButton : MonoBehaviour
{ 
    int roundNumber = 1;
    string roundComponentsGroupName;

    [SerializeField]
    string level;
    private int Score;
    List<string> clickedAudios = new List<string>(); //what the user clicked/chose
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
    private GameObject RoundComponents; 

    public UnityEngine.UI.ToggleGroup ToggleGroup;
    void Awake(){
        Debug.Log("Good Morning"); //debug
        setLevelNumber();
        if( ToggleGroup == null ) ToggleGroup = GetComponent<ToggleGroup>(); //make sure I have toggle group
        winSound = GetComponent<AudioSource>();
        
        roundComponentsGroupName = "Round1" + roundNumber;
        RoundComponents = GameObject.Find(roundComponentsGroupName); //had to move initialisation from the same line, refer: https://answers.unity.com/questions/502420/error-a-field-initializer-cannot-reference-the-non.html
    }

    int levelNumber;
    private void setLevelNumber(){
        if(level == "Easy")
        {levelNumber = 1;}
        else if(level == "Med")
        {levelNumber = 2;}
        else if(level == "Hard")
        {levelNumber = 3;}

        Debug.Log("level number is: " + levelNumber);
    }
    public void clickingToggle(){ //called every time a toggle is pressed
        string selectedToggle = ToggleGroup.ActiveToggles().FirstOrDefault().name; //adding the name of the button user clicks on 
        Debug.Log("logging the array: ");

        if(levelNumber == 1){
            clickedAudios.Clear(); //clear the array
            clickedAudios.Add(selectedToggle);
        }
        //displaying clickedAudios
        for(int i=0; i<clickedAudios.Count;i++){
            Debug.Log(clickedAudios[i]);
        }
    }

    int aims; //number of correct choices
    int miss; //number of wrong choices
    public void clickingSubmit(){
        Debug.Log("Hello from inside Submit");
        int i;
        for(i=0; i<levelNumber; i++)
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
        RoundComponents.SetActive(false);
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
        winSound.Play(0);
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
}
