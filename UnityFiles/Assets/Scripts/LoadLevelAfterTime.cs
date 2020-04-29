using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelAfterTime : MonoBehaviour
{
    [SerializeField]
    private float delayBeforeLoading;
    [SerializeField]
    private string sceneName;
    private float timeElapsed;
    
    //for global use
    static public int levelNo;

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if(timeElapsed > delayBeforeLoading)
        {
            if(sceneName == "Easy Level R1")
                levelNo = 1;
            else if(sceneName == "Medium Level R1")
                levelNo = 2;
            else
            {
                levelNo = 3;
            }
            SceneManager.LoadScene(sceneName);
        }
    }
}
