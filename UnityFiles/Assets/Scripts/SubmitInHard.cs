using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using System; //for error catching
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class SubmitInHard : MonoBehaviour
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
    public GameObject EndOfRoundWithEvaluation;

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

    //GameObject performRedirectionScript; //empty game object to act as a bridge between this script and Redirection.cs

    void Start()
    {
        Debug.Log("Good Morning from Hard :)");
        winSound = GetComponent<AudioSource>();

        clickedAudios = new List<string>();

        toggleGroup = GameObject.FindGameObjectsWithTag("ToggleButton");
        hearts = GameObject.FindGameObjectsWithTag("life");
        // int livesLeft = PlayerPrefs.GetInt("lives");
        // for(int i=0; i<livesLeft;i++)
        // {
        //     hearts[i].SetActive(true);
        // }

        showResults = GameObject.FindGameObjectsWithTag("evaluation");
        foreach (var item in showResults)
        {
            item.SetActive(false);
        }
    }

    // Update is called once per frame
    int ret = 0;
    void Update()
    {
        _score.GetComponent<Text>().text = "Score: " + Score.ToString();
        toggleGroup = GameObject.FindGameObjectsWithTag("ToggleButton");

        //when user gets less than 100% correct answer; 1/2 correct or 1/3 or 2/3 correct
        if (advanceToNextLevel == true)
        {
            //print("advancetolevel is true");
            if (ret != 1)
            {
                //ret = LoadLevelAfterTime("Hard Level Start");
                EndOfRoundWithEvaluation.SetActive(true);
                ret++;
            }
        }

        int livesLeft = PlayerPrefs.GetInt("lives");
        for (int i = 0; i < livesLeft; i++)
        {
            hearts[i].SetActive(true);
        }
    }

    //SET STUFF UP --------------------------
    public static int levelNumber = 3;
    string level = "Hard";


    int aims = 0; //number of correct choices
    int miss = 0; //number of wrong choices

    //SET STUFF UP DONE --------------------------
    Stack stackOfThree = new Stack(); //for hard level
    public void clickingToggleInHard()
    {
        GameObject activeToggle = EventSystem.current.currentSelectedGameObject;

        string activeToggleName = activeToggle.name; //just in case it gets changed inside the foreach loop
        if (stackOfThree.Count == 3)
        {
            foreach (var tgl in toggleGroup)
            {
                if (tgl != activeToggle)
                    tgl.GetComponent<Toggle>().isOn = false;
            }
            stackOfThree.Clear();
            clickedAudios.Clear(); //for submitButton function
        }
        stackOfThree.Push(activeToggleName);
        clickedAudios.Add(activeToggleName);
        print("stack has " + stackOfThree.Count + " elements now.");
        foreach (var obj in stackOfThree)
        {
            print(obj);
        }

        //2. get the name of the currently active toggle
        string selectedToggle = activeToggle.name;
        Debug.Log("active toggle: " + selectedToggle);

    }
    public void clickingSubmitInHard()
    {
        //ListenAgain is reset for every round
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
            if (aims == 3)
                userWins(3);
            else if (aims == 2)
                userWins(2);
            else if (aims == 1)
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
        //user wins less than 100% - gets half right or 1/3 right or 2/3 right answers
        else
        {
            performChecks();
        }

        //changing score
        if (numberOfCorrectAns == 3)
            Score = Score + 20;
        else if (numberOfCorrectAns == 2)
            Score = Score + 10;
        else
            Score = Score + 5;

        _score.GetComponent<Text>().text = "Score: " + Score.ToString();
        roundNumber++;
        print("round number from userWins " + roundNumber);

        SetStatistics(); //set score in playfab
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
        print("round number from userLoses: " + roundNumber);

        int count = hearts.Count();
        hearts[count - 1].SetActive(false); //the last heart is deactivated
        hearts = hearts.Take(hearts.Length - 1).ToArray(); //the array crops it out so next time the last element is a different heart
        print("hearts.Length: " + hearts.Length);
        PlayerPrefs.SetInt("lives", hearts.Length);

    }

    bool advanceToNextLevel = false;
    void performChecks()
    {
        foreach (var item in showResults)
        {
            //animate appearance of ticks and crosses:
            //1.set the size/scale of the ticks and crosses to x=0, y=0, z=0
            item.transform.localScale = new Vector3(0, 0, 0);
            //2.activate them
            item.SetActive(true);
            //3.make them increase size gradually till they reach scale: x=1,y=1,z=1
            LeanTween.scale(item, new Vector3(1, 1, 1), 0.8f);
        }
        foreach (var item in toggleGroup)
        {
            item.SetActive(false);
        }

        //automatically hide all buttons and move to next level
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("buttons");
        foreach (var btn in buttons)
        {
            btn.SetActive(false);
        }

        advanceToNextLevel = true;

        //adjust the lives
        int count = hearts.Count();
        hearts[count - 1].SetActive(false); //the last heart is deactivated
        hearts = hearts.Take(hearts.Length - 1).ToArray();
        print("hearts.Length: " + hearts.Length);
        PlayerPrefs.SetInt("lives", hearts.Length);
    }

    #region Getting Score from Playfab for display in game
    //called in Start() in both choice scene and test scene
    void GetStatistics()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            OnGetStatistics,
            error => Debug.LogError(error.GenerateErrorReport())
        );
    }

    void OnGetStatistics(GetPlayerStatisticsResult result)
    {
        Debug.Log("Received the following Statistics:");
        foreach (var eachStat in result.Statistics)
        {
            Debug.Log("Statistic (" + eachStat.StatisticName + "): " + eachStat.Value);
            if (eachStat.StatisticName == "Score")
            {
                Score = eachStat.Value;
            }
        }
    }
    #endregion

    #region Setting Score inside PlayFab
    //called after every change in score after clicking submit
    void SetStatistics()
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            // request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
            Statistics = new List<StatisticUpdate> {
        new StatisticUpdate { StatisticName = "Score", Value = Score },
        }
        },
        result => { Debug.Log("User score updated on playfab"); },
        error => { Debug.LogError(error.GenerateErrorReport()); });
    }
    #endregion

    #region Resettting Score when user exists game 
    //TODO: reset score to 0 after level is over .. or maybe don't and let levels be comprehensive. Think about it.
    #endregion
}

//References: 
//- How to Leaderboards in Playfab: https://www.youtube.com/watch?v=8rKARO5Z76g