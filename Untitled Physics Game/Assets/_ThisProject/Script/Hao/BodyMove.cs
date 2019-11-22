using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyMove : MonoBehaviour
{


    public float moveSpeed;
    float inputHor;
    float inputVer;


    void Update()
    {
        inputHor = Input.GetAxis("Horizontal");
        inputVer = Input.GetAxis("Vertical");

        this.transform.Translate(inputHor * moveSpeed * Time.deltaTime, inputVer * moveSpeed * Time.deltaTime, 0f);
    }

}
