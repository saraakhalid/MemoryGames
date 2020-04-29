using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAudiosInTurn : MonoBehaviour
{
    public GameObject Audio1;
    public GameObject Audio2;
    private string notifyPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Audio1.GetComponent<AudioSource>().Play();
        notifyPlayer = "first audio is playing ...";
        print(notifyPlayer);
        loadNextAudio();
    }
    void loadNextAudio(){

        StartCoroutine(WaitUntilFirstAudioStops());
    }
    IEnumerator WaitUntilFirstAudioStops(){
        Debug.Log("started waiting in coroutine");
        yield return new WaitForSeconds(Audio1.GetComponent<AudioSource>().clip.length+1);
        Debug.Log("finished waiting in coroutine");
        notifyPlayer = "";
        print(notifyPlayer);
        Audio2.GetComponent<AudioSource>().Play();
        notifyPlayer = "second audio is playing ...";
        print(notifyPlayer);
    }

    // void Update(){
    //     Debug.Log(notifyPlayer);
    // }
}
