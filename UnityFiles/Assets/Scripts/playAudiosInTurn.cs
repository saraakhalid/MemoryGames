using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playAudiosInTurn : MonoBehaviour
{
    public GameObject[] Audios;
    private string notifyPlayer;
    public GameObject whichPlayingAudio;
    // Start is called before the first frame update
    void Start()
    {
        Audios[0].GetComponent<AudioSource>().Play();
        notifyPlayer = "first audio is playing ...";
        // print(notifyPlayer);
        whichPlayingAudio.GetComponent<Text>().text = notifyPlayer;
        loadNextAudio();
    }
    void loadNextAudio(){

        StartCoroutine(WaitUntilFirstAudioStops());
    }
    IEnumerator WaitUntilFirstAudioStops(){
        yield return new WaitForSeconds(Audios[0].GetComponent<AudioSource>().clip.length+1);
        notifyPlayer = "";
        whichPlayingAudio.GetComponent<Text>().text = notifyPlayer;
        //print(notifyPlayer);
        Audios[1].GetComponent<AudioSource>().Play();
        notifyPlayer = "second audio is playing ...";
        //print(notifyPlayer);
        whichPlayingAudio.GetComponent<Text>().text = notifyPlayer;
        if(Audios[2]!=null)
        {
            yield return new WaitForSeconds(Audios[1].GetComponent<AudioSource>().clip.length+1);
            notifyPlayer = "";
            //print(notifyPlayer);
            whichPlayingAudio.GetComponent<Text>().text = notifyPlayer;
            Audios[2].GetComponent<AudioSource>().Play();
            notifyPlayer = "third audio is playing ...";
            // print(notifyPlayer);
            whichPlayingAudio.GetComponent<Text>().text = notifyPlayer;
        }
    }
}
