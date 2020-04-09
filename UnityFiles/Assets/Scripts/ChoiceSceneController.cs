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

    private void Awake() {
        foreach(var rnd in RoundComponents)
        {
            Debug.Log("Round number: ");
            Debug.Log(SubmitButton.roundNumber);
            
            if(rnd == RoundComponents[SubmitButton.roundNumber-1])
                {rnd.SetActive(true); //activate the second round components
                }
            else
                {
                    rnd.SetActive(false);
                    oldRound = rnd;
                }
        }
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // public void goNextRoundd(){
    //     SceneManager.GetActiveScene();
    //     Destroy(oldRound);
    // }
}
