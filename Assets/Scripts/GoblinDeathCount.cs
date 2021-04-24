using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoblinDeathCount : MonoBehaviour
{
    //倒したゴブリンの数表示
    public Text goblinCountText;
    public int goblinCount = 0;

    GoblinStatus goblinStatus;
    public FadeController fadeController;
    public GameObject missionCompleteText;
    //ミッション達成したかどうか
    public bool isMissionComplete;
    //ミッション達成サウンド
    AudioSource audioSource;
	public AudioClip missionCompleteSound; 

    private GameObject[] wolf;

    void Start()
    {
        this.goblinCountText.text = string.Format("{0} / 3", goblinCount);
        isMissionComplete = false;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CountGoblin(0);
    }

    //倒したゴブリンの数カウント
    public void CountGoblin(int addGoblin)
    {   
        this.goblinCount = goblinCount + addGoblin;
        this.goblinCountText.text = string.Format("{0} / 3", goblinCount);
        wolf = GameObject.FindGameObjectsWithTag("Wolf");
        //倒したゴブリンの数3以上なら次のミッションへ遷移
        if(goblinCount >= 3 && wolf.Length == 0 && isMissionComplete == false)
        {
            isMissionComplete = true;
            missionCompleteText.GetComponent<Text>().text = "Mission Complete";
            audioSource.PlayOneShot(missionCompleteSound);
            Invoke("ToMission2", 3);
        }
    }

    //ミッション2へ遷移
    void ToMission2()
	{
		fadeController.StartFadeOut();
		fadeController.changeSceneName = "Mission2";
	}
}
