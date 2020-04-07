using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
  public AudioMixer audiomixer;
  public void setVolume(float volume){
      audiomixer.SetFloat("music", volume); //set the "music" param in the audio mixer to whatever the input param is
      Debug.Log(volume);
  }
}
