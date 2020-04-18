using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using System.Linq;

public class SubmitButton : MonoBehaviour
{
    //todo: add time
    public static int roundNumber = 1;
    public static int Score;
    string roundComponentsGroupName;
    private GameObject[] toggleGroup; //a collection of all toggles in the scene of my creation
    [SerializeField]
    private GameObject _score;
    private GameObject[] hearts;
    private GameObject[] showResults;

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
    [SerializeField]
    private GameObject[] RoundComponents;

    void Awake()
    {

        Debug.Log("Good Morning"); //debug
        setLevelNumber();
        winSound = GetComponent<AudioSource>();

        clickedAudios = new List<string>();

        toggleGroup = GameObject.FindGameObjectsWithTag("ToggleButton");
        hearts = GameObject.FindGameObjectsWithTag("life");
        showResults = GameObject.FindGameObjectsWithTag("evaluation");
        foreach(var item in showResults){
            item.SetActive(false);
        }

    }
    void Update()
    {
        _score.GetComponent<Text>().text = "Score: " + Score.ToString();
        toggleGroup = GameObject.FindGameObjectsWithTag("ToggleButton");
        //if( ToggleGroup == null ) ToggleGroup = GetComponent<ToggleGroup>(); //make sure I have toggle group
    }
    public static int levelNumber;
    [SerializeField]
    string level;
    private void setLevelNumber()
    {
        if (level == "Easy")
        { levelNumber = 1; }
        else if (level == "Medium")
        { levelNumber = 2; }
        else if (level == "Hard")
        { levelNumber = 3; }

        Debug.Log("level number is: " + levelNumber);
    }

    int aims = 0; //number of correct choices
    int miss = 0; //number of wrong choices

    public void clickingSubmit()
    {
        Debug.Log("Hello from inside Submit");
        for (int i = 0; i < levelNumber; i++)
        {
            if (clickedAudios[i].IndexOf('*') != -1) //if the char * exists in the name of a button, it is the correct choice button
            {
                aims++;
                Debug.Log("Choice is correct! Aims: " + aims);
            }
            else
            {
                Debug.Log(clickedAudios[i]);
                miss++;
                Debug.Log("Choice is wrong! Miss: " + miss);
            }
        }

        if (levelNumber == 1)
        {
            if(aims == 1)
                userWins(1);
            else
                userLoses();
        }
        else if (levelNumber == 2)
        {
            if(aims == 2)
                userWins(2);
            else if(miss == 1)
                userWins(1);
            else
                userLoses();
        }
        else if (levelNumber == 3)
        {
            if(aims == 3)
                userWins(3);
            else if(miss == 1)
                userWins(2);
            else if(miss == 2)
                userWins(1);
            else
                userLoses();
        }
    }
    private void userWins(int numberOfCorrectAns)
    {
        if(levelNumber == numberOfCorrectAns){
            //displaying win/lose screen
        foreach (var rnd in RoundComponents)
        {
            if (rnd.activeSelf)
                rnd.SetActive(false);
        }
        EndOfRound.SetActive(true);
        print("You win this round!");
        txtEndOfRound.text = "You win this round! :)";
        thumbsUp.enabled = true;
        thumbsDown.enabled = false;
        winSound.Play();
        }
        else
        {
            foreach(var item in showResults)
            {
                item.SetActive(true);
            }
            foreach(var item in toggleGroup)
            {
                item.SetActive(false);
            }
        }
        switch(levelNumber){
            case 1:
            Score = Score + 5;
            break;

            case 2:
            if(numberOfCorrectAns == 2)
                Score = Score + 10;
            else
                Score = Score + 5;
            break;

            case 3:
            if(numberOfCorrectAns == 3)
                Score = Score + 20;
            else if(numberOfCorrectAns == 2)
                Score = Score + 10;
            else
                Score = Score + 5;
            break;
        }

        // Score = Score + 10;
        _score.GetComponent<Text>().text = "Score: " + Score.ToString();
        roundNumber++;
    }
    private void userLoses()
    {
        foreach (var rnd in RoundComponents)
        {
            if (rnd.activeSelf)
                rnd.SetActive(false);
        }
        EndOfRound.SetActive(true);
        print("You lose this round!");
        txtEndOfRound.text = "You lose this round! :(";
        thumbsUp.enabled = false;
        thumbsDown.enabled = true;
        roundNumber++;
        int count = hearts.Count();
        hearts[count-1].SetActive(false); //the last heart is deactivated
        hearts = hearts.Take(hearts.Length - 1).ToArray(); //the array crops it out so next time the last element is a different heart
        
    }

    /* variables for clickingToggle */
    Stack stackOfTwo = new Stack(); //for med level
    Stack stackOfThree = new Stack(); //for hard level

    public void clickingToggle()
    { //called every time a toggle is presseds

        GameObject activeToggle = EventSystem.current.currentSelectedGameObject;
        List<GameObject> threeActiveToggles = new List<GameObject>(); //for Hard level

        //1. activate only the clicked toggle and deactivate all other toggles in the screen
        if (levelNumber == 1)
        {
            foreach (var tgl in toggleGroup)
            {
                if (tgl == activeToggle)
                {
                    tgl.GetComponent<Toggle>().isOn = true;
                }
                else
                    tgl.GetComponent<Toggle>().isOn = false;
            }
        }
        else if (levelNumber == 2)
        {
            string activeToggleName = activeToggle.name;
            if(stackOfTwo.Count == 2)
            {
                foreach(var tgl in toggleGroup)
                {
                    if(tgl != activeToggle)
                        tgl.GetComponent<Toggle>().isOn = false;
                }
                stackOfTwo.Clear(); //this has to be beneath the foreach loop or otherwise the algorithm does not work!!
                clickedAudios.Clear();
            }
            stackOfTwo.Push(activeToggleName);
            clickedAudios.Add(activeToggleName);
            print("stack has " + stackOfTwo.Count + " elements now.");
            foreach(var obj in stackOfTwo){
                print(obj);
            }
        }
        else if (levelNumber == 3)
        {
            string activeToggleName = activeToggle.name; //just in case it gets changed inside the foreach loop
            if(stackOfThree.Count == 3)
            {
                foreach(var tgl in toggleGroup)
                {
                    if(tgl != activeToggle)
                        tgl.GetComponent<Toggle>().isOn = false;
                }
                stackOfThree.Clear();
                clickedAudios.Clear(); //for submitButton function
            }   
            stackOfThree.Push(activeToggleName);
            clickedAudios.Add(activeToggleName);
            print("stack has " + stackOfThree.Count + " elements now.");
            foreach(var obj in stackOfThree){
                print(obj);
            }
        }

        //2. get the name of the currently active toggle
        string selectedToggle = activeToggle.name;

        Debug.Log("active toggle: " + selectedToggle);
        // print(selectedToggle);

        //3. add it to an array of user answers for validation when submitting
        if (levelNumber == 1)
        {
            clickedAudios.Clear(); //clear the array
            clickedAudios.Add(selectedToggle); //add the name of the toggle to the array
        }

        //debugging ....
        //displaying clickedAudios
        // print("displaying clicked audios...");
        // for (int i = 0; i < clickedAudios.Count; i++)
        // {
        //     Debug.Log(clickedAudios[i]);
        // }
    }

    public void goToNextRound() //unused
    {
        EndOfRound.SetActive(false);

        if (roundNumber == 2)
            RoundComponents[1].SetActive(true); //activate the second round components
        else if (roundNumber == 3)
            RoundComponents[2].SetActive(true);
    }
}