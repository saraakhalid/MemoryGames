﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] RoundComponents;
    [SerializeField]
    private GameObject _score;
    private void Awake()
    {
        _score.GetComponent<Text>().text = "Score: " + SubmitButton.Score.ToString();

        foreach (var rnd in RoundComponents)
        {
            if (rnd == RoundComponents[SubmitButton.roundNumber - 1])
                rnd.SetActive(true); //activate the second round components
            else
                rnd.SetActive(false);
        }
    }
}
