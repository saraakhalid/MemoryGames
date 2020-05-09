using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
public class StartScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Login.isLoggedIn == true)
        {
            GameObject RegisterButton = GameObject.Find("btnRegister");
            RegisterButton.SetActive(false); //hide the register button is user is already logged in
        }
    }

    #region Leaderboard
    public void GetLeaderboarder()
    {
        var requestLeaderboard = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "Score" };
        PlayFabClientAPI.GetLeaderboard(requestLeaderboard, OnGetLeadboard, OnErrorLeaderboard);
    }

    void OnGetLeadboard(GetLeaderboardResult result)
    {
        Debug.Log(result.Leaderboard[0].StatValue);
        foreach (PlayerLeaderboardEntry player in result.Leaderboard)
        {
            Debug.Log(player.DisplayName + ": " + player.StatValue);
        }
    }
    void OnErrorLeaderboard(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }
    #endregion
}
