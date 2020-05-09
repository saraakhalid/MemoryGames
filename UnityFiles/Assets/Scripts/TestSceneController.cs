using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class TestSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] RoundComponents;
    [SerializeField]
    private GameObject _score;
    private void Awake()
    {
        Debug.Log("Awake from testSceneController ");

        #region Setting Up the score 
        int LevelNumber = LoadLevelAfterTime.levelNo;

        if (LevelNumber == 1)
        {
            _score.GetComponent<Text>().text = "Score: " + SubmitButton.Score.ToString();
            print("score is set for level 1");
        }
        else if (LevelNumber == 2)
        {
            _score.GetComponent<Text>().text = "Score: " + SubmitInMedium.Score.ToString();
            print("score is set for level 2");
        }
        else if (LevelNumber == 3)
        {
            _score.GetComponent<Text>().text = "Score: " + SubmitInHard.Score.ToString();
            print("score is set for level 3");
        }
        #endregion

        foreach (var rnd in RoundComponents)
        {
            if (LoadLevelAfterTime.levelNo == 1)
            {
                print("I'm passing through TestSceneController");
                if (rnd == RoundComponents[SubmitButton.roundNumber - 1])
                    rnd.SetActive(true); //activate the second round components
                else
                    rnd.SetActive(false);
            }
            else if (LoadLevelAfterTime.levelNo == 2)
            {
                print("I'm passing through TestSceneController");
                if (rnd == RoundComponents[SubmitInMedium.roundNumber - 1])
                    rnd.SetActive(true); //activate the second round components
                else
                    rnd.SetActive(false);
            }
            else if (LoadLevelAfterTime.levelNo == 3)
            {
                if (rnd == RoundComponents[SubmitInHard.roundNumber - 1])
                    rnd.SetActive(true); //activate the second round components
                else
                    rnd.SetActive(false);
            }
        }
    }
    //May need it later
    // #region Getting Score from Playfab for display in game
    // //called in Start() in both choice scene and test scene
    // void GetStatistics()
    // {
    //     PlayFabClientAPI.GetPlayerStatistics(
    //         new GetPlayerStatisticsRequest(),
    //         OnGetStatistics,
    //         error => Debug.LogError(error.GenerateErrorReport())
    //     );
    // }

    // void OnGetStatistics(GetPlayerStatisticsResult result)
    // {
    //     Debug.Log("Received the following Statistics:");
    //     foreach (var eachStat in result.Statistics)
    //     {
    //         Debug.Log("Statistic (" + eachStat.StatisticName + "): " + eachStat.Value);
    //         if (eachStat.StatisticName == "Score")
    //         {
    //             //Score = eachStat.Value;

    //         }
    //     }
    // }
    // #endregion
}
