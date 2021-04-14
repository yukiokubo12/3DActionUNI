using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSystemScript : MonoBehaviour
{
    // ボタンをインスペクタからアタッチする場合
    public Button howToPlayButton;
    public GameObject howToPlayText;
    // 表示中かどうかのステート
    private bool isDisplay = false;
    // 外部から表示中かどうかを見たい場合のため用意
    public bool IsDisplay => isDisplay;
    private void Start()
    {
        // ボタンをアタッチしている場合は押したらトグルが呼ばれるようにする
        howToPlayButton?.onClick.AddListener(ToggleText);
    }
    // ボタンが押された際に呼ばれる
    public void ToggleText()
    {
        if (isDisplay)
        {
            // 表示中なら非表示へ
            HideText();
        }
        else
        {
            // 非表示中なら表示へ
            ShowText();
        }
    }
    //表示, 非表示のボタンを結局別で用意することになった場合のため、public
    public void ShowText()
    {
        howToPlayText.GetComponent<Text>().text = "[キーパッド]\n←↑→ 移動\nShift 走る\nSpace ジャンプ\nE 攻撃\n\n[ゲームパッド]\n左スティック 移動\n左スティック カメラ操作\n右スティック カメラ操作\nL2 走る\n☓ ジャンプ\n○ 攻撃";
        isDisplay = true;
    }
    public void HideText()
    {
       // 非表示時中は何も入れない
       howToPlayText.GetComponent<Text>().text = "";
       isDisplay = false;
    }
}