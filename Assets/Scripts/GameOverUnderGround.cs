using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//プレイヤーが地面をすり抜けた（バグ）時の対処
public class GameOverUnderGround : MonoBehaviour
{
    public GameObject gameOverText;
    public FadeController fadeController;

    //プレイヤーとの当たり判定
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            gameOverText.GetComponent<Text>().text = "Game Over";
            Invoke("ToTitleScene", 3);
        }
    }
    //タイトルシーンへ遷移
    void ToTitleScene()
	{
		fadeController.StartFadeOut();
		fadeController.changeSceneName = "Title";
	}
}
