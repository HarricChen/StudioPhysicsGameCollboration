using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomRotate : MonoBehaviour
{
    public int rotateAngle;

    private void Update()
    {
       ControlRotate();
    }

    private void ControlRotate()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            this.gameObject.transform.Rotate(0f, 0f, rotateAngle);
        }

        else if(Input.GetKeyDown(KeyCode.Q))
        {
            this.gameObject.transform.Rotate(0f, 0f, -rotateAngle);
        }
    }
    

}
