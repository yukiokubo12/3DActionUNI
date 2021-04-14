using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSystemScript : MonoBehaviour
{
    public Button howToPlayButton;
    public GameObject howToPlayText;
    // public Button toMainButton;
    // public Button toTitleButton;
    // public Text clearText;

    public void HowToPlay()
    {
        
        this.howToPlayText.GetComponent<Text>().text = "[キーパッド]\n←↑→ 移動\nShiftキー 走る\nSpace ジャンプ\nE→攻撃\n\n[ゲームパッド]\n左スティック 移動\n右スティック カメラ操作\n 移動\n右スティック カメラ操作\nL2 走る\n☓ ジャンプ\n○ 攻撃";
    }
    // public void StartGame()
    // {
    //     SceneManager.LoadScene("Main");
    // }
    // public void ToTitle()
    // {
    //     SceneManager.LoadScene("Title");
    // }
    // public void ShowMainButton()
    // {
    //     this.toMainButton.gameObject.SetActive(true);
    // }
    // public void ShowTitleButton()
    // {
    //     this.toTitleButton.gameObject.SetActive(true);
    // }
    // public void ShowClearText()
    // {
    //     this.clearText.GetComponent<Text>().text = "Game Clear!!";
    //     this.clearText.gameObject.SetActive(true);
    // }
}