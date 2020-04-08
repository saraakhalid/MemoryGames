using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

public class AudioButtonsControl : MonoBehaviour
{
    private AudioSource[] allAudioSources; //all audio sources in the scene 
    private AudioSource allAudioSourcesPlayingRightNow;
    //List<AudioSource> allAudioSourcesPlayingRightNow;
    

    void Awake(){
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
    }
        public void clickingAudioButton(){
            //1. get name of the child of this button
            var childName = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).name;
            Debug.Log("childName: ");
            Debug.Log(childName);

            //2. store it in var of current playing sounds
            foreach(var aud in allAudioSources){
                if(aud.name == childName){
                    allAudioSourcesPlayingRightNow = aud;
                }
            }

            Debug.Log("allAudioSourcesPlayingRightNow");
            Debug.Log(allAudioSourcesPlayingRightNow);

            //3. stop playing all sounds in the array when button is clicked except clicked sound
            foreach(var audio in allAudioSources)
            {
                if(audio == allAudioSourcesPlayingRightNow)
                {
                    audio.Play();
                }
                else
                {
                    audio.Stop();
                }
            }

    }
     
    public void StopAllAudio() {
     foreach( AudioSource audioS in allAudioSources) {
         audioS.Stop();
         }
 }
}
