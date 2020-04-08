using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChoiceSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] RoundComponents;

    private void Awake() {
        foreach(var rnd in RoundComponents)
        {
            Debug.Log("Round number: ");
            Debug.Log(SubmitButton.roundNumber);
            
            if(rnd == RoundComponents[SubmitButton.roundNumber-1])
                rnd.SetActive(true); //activate the second round components
            else
                rnd.SetActive(false);
        }
    }
    //Find out which round user is in before doing anything
}
