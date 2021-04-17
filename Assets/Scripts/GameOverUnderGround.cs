using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUnderGround : MonoBehaviour
{
    public GameObject gameOverText;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            gameOverText.GetComponent<Text>().text = "Game Over";
            SceneManager.LoadScene("Title");
        }
    }
}
