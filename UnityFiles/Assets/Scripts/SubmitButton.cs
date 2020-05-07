using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using System; //for error catching

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
        //showResults = GameObject.FindGameObjectsWithTag("evaluation");
        foreach (var item in showResults)
        {
            item.SetActive(false);
        }

    }
    void Update()
    {
        _score.GetComponent<Text>().text = "Score: " + Score.ToString();
        toggleGroup = GameObject.FindGameObjectsWithTag("ToggleButton");

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

        int resetListenAgain = 0;
        PlayerPrefs.SetInt("times listened", resetListenAgain);
        try
        {
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

            if (aims == 1)
                userWins(1);
            else
                userLoses();
        }
        catch (Exception e)
        {
            print("Please select one of the choices!");
        }
    }
    private void userWins(int numberOfCorrectAns)
    {
        //user wins 100%
        if (levelNumber == numberOfCorrectAns)
        {
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
            Score = Score + 5;

        // Score = Score + 10;
        _score.GetComponent<Text>().text = "Score: " + Score.ToString();
        roundNumber++;
    }
    private void Destroy()
    {
        Destroy(gameObject);
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
        hearts[count - 1].SetActive(false); //the last heart is deactivated
        hearts = hearts.Take(hearts.Length - 1).ToArray(); //the array crops it out so next time the last element is a different heart

    }


    /* variables for clickingToggle */
    // Stack stackOfTwo = new Stack(); //for med level


    public void clickingToggle()
    { //called every time a toggle is presseds

        GameObject activeToggle = EventSystem.current.currentSelectedGameObject;
        //List<GameObject> threeActiveToggles = new List<GameObject>(); //for Hard level

        //1. activate only the clicked toggle and deactivate all other toggles in the screen
            foreach (var tgl in toggleGroup)
            {
                if (tgl == activeToggle)
                {
                    tgl.GetComponent<Toggle>().isOn = true;
                }
                else
                    tgl.GetComponent<Toggle>().isOn = false;
            }

        //2. get the name of the currently active toggle
        string selectedToggle = activeToggle.name;

        Debug.Log("active toggle: " + selectedToggle);
        // print(selectedToggle);

        //3. add it to an array of user answers for validation when submitting
            clickedAudios.Clear(); //clear the array
            clickedAudios.Add(selectedToggle); //add the name of the toggle to the array

        //debugging ....
        //displaying clickedAudios
        // print("displaying clicked audios...");
        // for (int i = 0; i < clickedAudios.Count; i++)
        // {
        //     Debug.Log(clickedAudios[i]);
        // }
    }

    // public void goToNextRound() //unused
    // {
    //     print("inside gotonextround..");
    //     EndOfRound.SetActive(false);

    //     if (roundNumber == 2)
    //         RoundComponents[1].SetActive(true); //activate the second round components
    //     else if (roundNumber == 3)
    //         RoundComponents[2].SetActive(true);
    // }
}