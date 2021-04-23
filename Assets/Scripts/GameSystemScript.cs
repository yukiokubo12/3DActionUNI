using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSystemScript : MonoBehaviour
{
    //操作説明ボタン、テキスト
    public Button howToPlayButton;
    public GameObject howToPlayText;
    //ミッション説明ボタン、テキスト
    public Button missionButton;
    public GameObject missionText;
    //ミッション用タイマー
    public GameObject timerText;
    // 表示中かどうかのステート
    private bool isDisplay = false;
    // 外部から表示中かどうかを見たい場合のため用意
    public bool IsDisplay => isDisplay;
    //ミッション内容をシーンによって変更
    [SerializeField] string m_missionDescription = "ミッション内容を設定する";

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

    //操作説明テキスト表示
    public void ShowHowToText()
    {
        howToPlayText.GetComponent<Text>().text = "[キーパッド]\n←↑→ 移動\nShift 走る\nSpace ジャンプ\nE 攻撃\n\n[ゲームパッド]\n左スティック 移動\n右スティック カメラ操作\nL2 走る\n☓ ジャンプ\n○ 攻撃";
        isDisplay = true;
    }

    //操作説明テキスト非表示
    public void HideHowToText()
    {
       howToPlayText.GetComponent<Text>().text = "";
       isDisplay = false;
    }

    //ミッションテキスト表示
    public void ShowMissionText()
    {
        missionText.GetComponent<Text>().text = m_missionDescription;
        isDisplay = true;
    }

    //ミッションテキスト非表示
    public void HideMissionText()
    {
       missionText.GetComponent<Text>().text = "";
       isDisplay = false;
    }

    //ミッション用タイマー表示
    public void ShowTimerText()
    {
        timerText.GetComponent<Text>();
        isDisplay = true;
    }

    //ミッション２シーンへ遷移
    public void ToGameMission2()
    {
        SceneManager.LoadScene("Mission2");
    }
}