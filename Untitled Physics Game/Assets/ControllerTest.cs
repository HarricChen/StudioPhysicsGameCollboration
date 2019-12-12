using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTest : MonoBehaviour
{

    public KeyCode key1;
    public KeyCode key2;

    private void Update()
    {
        if(Input.GetKey(key1))
        {
            Debug.Log("The Key1 is being Pressed");
        }

        if(Input.GetKey(key2))
        {
            Debug.Log("The Key2 is being Pressed");
        }
    }
}
