using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBetweenRounds : MonoBehaviour
{
    //when user clicks SUBMIT, the following happens:
    public void checkChoices(){

        //1. check if the choices match the audios that were played in the beginning
        //2. place tick over correct choices and cross over wrong ones
        //3. deactivate "Submit" and "Listen again" buttons
        //4. activate "Next Round" button
        //5. increment the round counter by one - indicating that we have finished a round
        //TODO: incrementation of round counter triggers changing audios between rounds, changing the title text of the screen and keeping the score showing as it is without resetting it. This can be done inside one function called GoNextRound()
    }
}
