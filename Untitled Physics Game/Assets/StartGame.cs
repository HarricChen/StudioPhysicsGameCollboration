using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Color color;
    public float startGameTime;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {
            if (other.tag == "Start")
            {
                Debug.Log("Start Enter");
                sprite.color = new Color(color.r, color.g, color.b);
                SceneManager.LoadScene(1);
            }

            if(other.tag == "Exit")
            {
                Debug.Log("Exit Enter");
                sprite.color = new Color(color.r, color.g, color.b);
                Application.Quit();
            }

        }
    }

}
