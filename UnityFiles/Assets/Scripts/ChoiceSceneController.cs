using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChoiceSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] RoundComponents;
    private GameObject oldRound;
    private GameObject[] hearts;

    private void Awake() {
        hearts = GameObject.FindGameObjectsWithTag("life");

        foreach(var rnd in RoundComponents)
        {
            if(LoadLevelAfterTime.levelNo == 1)
            {
                Debug.Log("Round number: ");
                Debug.Log(SubmitButton.roundNumber);
            
                if(rnd == RoundComponents[SubmitButton.roundNumber-1])
                    {
                        rnd.SetActive(true); //activate the second round components

                        //retrieve lives
                        int livesLeft = PlayerPrefs.GetInt("lives");
                        for(int i=0; i<livesLeft;i++)
                        {
                            hearts[i].SetActive(true);
                        }
                    }
                else
                    {
                        rnd.SetActive(false);
                        oldRound = rnd;
                    }
            }
            else if(LoadLevelAfterTime.levelNo == 2)
            {
                Debug.Log("Round number: ");
                Debug.Log(SubmitInMedium.roundNumber);
            
            if(rnd == RoundComponents[SubmitInMedium.roundNumber-1])
                {rnd.SetActive(true); //activate the second round components
                }
            else
                {
                    rnd.SetActive(false);
                    oldRound = rnd;
                }
            }
            else if(LoadLevelAfterTime.levelNo == 3)
            {
                Debug.Log("Round number: ");
                Debug.Log(SubmitInHard.roundNumber);
            
            if(rnd == RoundComponents[SubmitInHard.roundNumber-1])
                {rnd.SetActive(true); //activate the second round components
                }
            else
                {
                    rnd.SetActive(false);
                    oldRound = rnd;
                }
            }
        }
    }
}
