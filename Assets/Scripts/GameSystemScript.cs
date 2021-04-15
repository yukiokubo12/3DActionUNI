using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSystemScript : MonoBehaviour
{

    public Button nextMissionButton;
    public Text clearText;

    // ボタンをインスペクタからアタッチする場合
    public Button howToPlayButton;
    public GameObject howToPlayText;

    public Button missionButton;
    public GameObject missionText;
    public GameObject timerText;
    // 表示中かどうかのステート
    private bool isDisplay = false;
    // 外部から表示中かどうかを見たい場合のため用意
    public bool IsDisplay => isDisplay;
    private void Start()
    {
        // ボタンをアタッチしている場合は押したらトグルが呼ばれるようにする
        howToPlayButton?.onClick.AddListener(ToggleHowToText);
        missionButton?.onClick.AddListener(ToggleMissionText);
        
    }
    // ボタンが押された際に呼ばれる
    public void ToggleHowToText()
    {
        if (isDisplay)
        {
            // 表示中なら非表示へ
            HideHowToText();
        }
        else
        {
            // 非表示中なら表示へ
            ShowHowToText();
        }
    }
    public void ToggleMissionText()
    {
        if (isDisplay)
        {
            // 表示中なら非表示へ
            HideMissionText();
        }
        else
        {
            // 非表示中なら表示へ
            ShowMissionText();
            ShowTimerText();
        }
    }
    //表示, 非表示のボタンを結局別で用意することになった場合のため、public
    public void ShowHowToText()
    {
        howToPlayText.GetComponent<Text>().text = "[キーパッド]\n←↑→ 移動\nShift 走る\nSpace ジャンプ\nE 攻撃\n\n[ゲームパッド]\n左スティック 移動\n左スティック カメラ操作\n右スティック カメラ操作\nL2 走る\n☓ ジャンプ\n○ 攻撃";
        isDisplay = true;
    }
    public void HideHowToText()
    {
       // 非表示時中は何も入れない
       howToPlayText.GetComponent<Text>().text = "";
       isDisplay = false;
    }
    public void ShowMissionText()
    {
        missionText.GetComponent<Text>().text = "敵を５体倒せ";
        // missionText.GetComponent<Text>().text = "3分以内にゴブリンを５体倒せ";
        isDisplay = true;
    }
    public void HideMissionText()
    {
       // 非表示時中は何も入れない
       missionText.GetComponent<Text>().text = "";
       isDisplay = false;
    }
    public void ShowTimerText()
    {
        timerText.GetComponent<Text>();
        isDisplay = true;
    }
    // public void HideTimerText()
    // {
    //    timerText.GetComponent<Text>().text = "";
    //    isDisplay = false;
    // }

    public void ToGameMission2()
    {
        SceneManager.LoadScene("GameMission2");
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