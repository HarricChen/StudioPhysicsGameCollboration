using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodePosition : MonoBehaviour
{
    GameObject _player;

    [SerializeField] private KeyCode _cheatCode;

    private void Start()
    {
        _player = GameObject.Find("Player");
    }

    private void Update()
    {
        if(Input.GetKeyDown(_cheatCode))
        {
            _player.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
        }
    }


}
