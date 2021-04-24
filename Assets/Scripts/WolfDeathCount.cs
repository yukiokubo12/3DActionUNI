using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WolfDeathCount : MonoBehaviour
{
    //倒したウルフの数表示
    public Text wolfCountText;
    private int wolfCount = 0;
    //ミッション達成したかどうか
    public bool isMission2Complete;
    public GameObject missionCompleteText;
    //ミッション達成サウンド
    AudioSource audioSource;
	public AudioClip missionCompleteSound; 

    public FadeController fadeController;

    void Start()
    {
        this.wolfCountText.text = string.Format("{0} / 4", wolfCount);
        isMission2Complete = false;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CountWolf(0);
    }

    //倒したウルフの数カウント
    public void CountWolf(int addWolf)
    {   
        this.wolfCount = wolfCount + addWolf;
        this.wolfCountText.text = string.Format("{0} / 4", wolfCount);
        //倒したウルフの数4以上なら次のミッションへ遷移
        if(wolfCount >= 4 && isMission2Complete == false)
        {
            isMission2Complete = true;
            missionCompleteText.GetComponent<Text>().text = "Mission Complete";
            audioSource.PlayOneShot(missionCompleteSound);
            Invoke("ToMission3", 3);
        }
    }

    //ミッション3へ遷移
    void ToMission3()
	{
		fadeController.StartFadeOut();
		fadeController.changeSceneName = "Mission3";
	}
}