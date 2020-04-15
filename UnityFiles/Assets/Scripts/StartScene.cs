using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Login.isLoggedIn == true)
        {
            GameObject RegisterButton = GameObject.Find("btnRegister");
            RegisterButton.SetActive(false); //hide the register button is user is already logged in
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
