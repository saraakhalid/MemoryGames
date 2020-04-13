using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndOfLevel : MonoBehaviour
{
    [SerializeField]
    private GameObject totalScore;
    [SerializeField]
    private GameObject[] stars;
    // Start is called before the first frame update
    void Start()
    {
        //stars = GameObject.FindGameObjectsWithTag("star"); //does not work
        Debug.Log(stars); //debugging
        totalScore.GetComponent<Text>().text = "Score: " + SubmitButton.Score.ToString();
        if (SubmitButton.Score == 0)
        {
            foreach (var s in stars)
            {
                s.SetActive(false);
            }
        }
        else if (SubmitButton.Score <= 10)
        {
            stars[0].SetActive(true);
        }
        else if (SubmitButton.Score > 10 && SubmitButton.Score <= 20)
        {
            stars[0].SetActive(true);
            stars[1].SetActive(true);
        }
        else if (SubmitButton.Score > 20 && SubmitButton.Score <= 30)
        {
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            stars[2].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
