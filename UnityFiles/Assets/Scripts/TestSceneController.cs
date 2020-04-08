using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] RoundComponents;
    private void Awake() {
        foreach(var rnd in RoundComponents)
        {
            if(rnd == RoundComponents[SubmitButton.roundNumber-1])
                rnd.SetActive(true); //activate the second round components
            else
                rnd.SetActive(false);
        }
    }
}
