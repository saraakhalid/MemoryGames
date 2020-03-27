using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHelp : MonoBehaviour
{
    public GameObject HelpPanel;

    public void OpenGameInformation()
    {
        if (HelpPanel != null)
        {
            HelpPanel.SetActive(true);
        }
    }

    public void ClosePanel()
    {
        if (HelpPanel != null)
        {
            //find out which one to activate
            if (HelpPanel.activeSelf == true)
            {
                HelpPanel.SetActive(false); //deactivate the panel
                print("I'm doing my job");
            }
            else
                print("something is wrong"); //for debug
        }
        //reference for panel functions: https://docs.unity3d.com/Manual/UpgradeGuide3540.html
    }
}
