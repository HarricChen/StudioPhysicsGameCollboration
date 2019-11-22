using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHorizontal : MonoBehaviour
{

    float _horiInput;
    public float speed;

    public string horizontalMove;

    private void Update()
    {
        _horiInput = Input.GetAxis(horizontalMove);

        this.transform.Translate(_horiInput * speed * Time.deltaTime, 0f, 0f);
    }

}
