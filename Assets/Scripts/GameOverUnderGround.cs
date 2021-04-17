using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUnderGround : MonoBehaviour
{
    public GameObject gameOverText;
    public FadeController fadeController;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            gameOverText.GetComponent<Text>().text = "Game Over";
            Invoke("ToTitleScene", 3);
        }
    }

    void ToTitleScene()
	{
		fadeController.StartFadeOut();
		fadeController.changeSceneName = "Title";
	}
}
