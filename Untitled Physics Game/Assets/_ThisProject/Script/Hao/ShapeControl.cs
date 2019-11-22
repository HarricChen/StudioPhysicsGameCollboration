using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeControl : MonoBehaviour
{
    public KeyCode downKey;
    public KeyCode upKey;
    public Animator playerAni;


    private void Update()
    {
        if(Input.GetKeyDown(downKey))
        {
            playerAni.SetBool("IsDown", true);
        }

        if (Input.GetKeyUp(downKey))
        {
            playerAni.SetBool("IsDown", false);
        }

        if(Input.GetKeyDown(upKey))
        {
            playerAni.SetBool("IsUp", true);
        }

        if(Input.GetKeyUp(upKey))
        {
            playerAni.SetBool("IsUp", false);
        }

    }

}
