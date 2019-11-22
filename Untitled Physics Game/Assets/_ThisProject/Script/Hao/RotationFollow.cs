using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFollow : MonoBehaviour
{
    public Transform target;
    public float followSpeed;

    private void Update()
    {
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, target.rotation, followSpeed * Time.deltaTime);
    }

}
