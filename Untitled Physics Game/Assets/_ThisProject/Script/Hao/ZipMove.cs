using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZipMove : MonoBehaviour
{

    [SerializeField]
    Transform _targetPos;

    [SerializeField]
    float _moveSpeed;

    public bool isMove;


    void Update()
    {
        if(isMove == true)
        {
            this.transform.position = Vector2.Lerp(this.transform.position, _targetPos.position, _moveSpeed * Time.deltaTime);
        }
    }
}
