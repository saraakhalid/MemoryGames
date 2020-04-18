using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAudiosInTurn : MonoBehaviour
{
    public GameObject Audio1;
    public GameObject Audio2;
    // Start is called before the first frame update
    void Start()
    {
        Audio1.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Audio1.GetComponent<AudioSource>().isPlaying)
            Audio2.GetComponent<AudioSource>().Play();
    }
}
