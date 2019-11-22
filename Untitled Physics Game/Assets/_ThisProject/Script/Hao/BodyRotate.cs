using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyRotate : MonoBehaviour
{
    public float rotateSpeed;

    private void Update()
    {
        SelfRotate();
    }

    void SelfRotate()
    {
        this.transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }
}
