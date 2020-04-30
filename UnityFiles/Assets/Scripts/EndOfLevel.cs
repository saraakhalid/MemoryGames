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

        totalScore.GetComponent<Text>().text = "Score: " + SubmitButton.Score.ToString();
        if (SubmitButton.Score == 0)
        {
            foreach (var s in stars)
            {
                s.SetActive(false);
            }
        }
        if(LoadLevelAfterTime.levelNo == 1)
            endOfEasyLevel();
        else if(LoadLevelAfterTime.levelNo == 2)
            endOfMediumLevel();
        else
            endOfHardLevel();
    }

    // Update is called once per frame
    private void endOfEasyLevel(){
            if(SubmitButton.Score <= 5)
            {
                stars[1].SetActive(true);
            }
            else if (SubmitButton.Score > 5 && SubmitButton.Score <= 10)
        {
            stars[0].SetActive(true);
            stars[1].SetActive(true);
        }
        else if (SubmitButton.Score > 10 && SubmitButton.Score <= 15)
        {
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            stars[2].SetActive(true);
        }
    }

    private void endOfMediumLevel(){
            if(SubmitInMedium.Score <= 10)
            {
            stars[1].SetActive(true);
            }
            else if(SubmitInMedium.Score > 10 && SubmitInMedium.Score <= 20)
            {
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            }
            else if(SubmitInMedium.Score > 20 && SubmitInMedium.Score <= 30)
            {
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            stars[2].SetActive(true);
            }
    }
    private void endOfHardLevel(){
            if(SubmitInHard.Score <= 20)
            {
            stars[1].SetActive(true);
            }
            else if(SubmitInHard.Score > 20 && SubmitInHard.Score <= 40)
            {
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            }
            else if(SubmitInHard.Score > 40 && SubmitInHard.Score <= 60)
            {
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            stars[2].SetActive(true);                
            }
    }
}
