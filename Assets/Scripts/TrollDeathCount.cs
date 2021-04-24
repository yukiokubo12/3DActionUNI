using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrollDeathCount : MonoBehaviour
{
    //倒したトロールの数表示
    public Text trollCountText;
    private int trollCount = 0;
    //ミッション達成したかどうか
    public bool isMission3Complete;
    public GameObject missionCompleteText;
    //ミッション達成サウンド
    AudioSource audioSource;
	public AudioClip missionCompleteSound; 

    public FadeController fadeController;

    void Start()
    {
        this.trollCountText.text = string.Format("{0} / 1", trollCount);
        isMission3Complete = false;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CountTroll(0);
    }

    //倒したトロールの数カウント
    public void CountTroll(int addTroll)
    {   
        this.trollCount = trollCount + addTroll;
        this.trollCountText.text = string.Format("{0} / 1", trollCount);
        //倒したトロールの数１以上なら次のミッションへ遷移
        if(trollCount >= 1 && isMission3Complete == false)
        {
            isMission3Complete = true;
            missionCompleteText.GetComponent<Text>().text = "Mission Complete";
            audioSource.PlayOneShot(missionCompleteSound);
            Invoke("ToEnd", 3);
        }
    }

    //エンドシーンへ遷移
    void ToEnd()
	{
		fadeController.StartFadeOut();
		fadeController.changeSceneName = "End";
	}
}











// {
//     //トロール数える
//     private GameObject[] trollObject;
//     //ミッションクリアしたかどうか
//     private bool isMission3Complete;
//     //ミッションコンプリートテキスト
//     public GameObject missionCompleteText;
//     //サウンド
//     AudioSource audioSource;
// 	public AudioClip missionCompleteSound; 

//     public FadeController fadeController;

//     void Start()
//     {
//         isMission3Complete = false;
//         audioSource = GetComponent<AudioSource>();
//     }
//     void Update()
//     {
//         trollObject = GameObject.FindGameObjectsWithTag("Troll");
//         if(trollObject.Length == 0 && isMission3Complete == false)
//         {
//             isMission3Complete = true;
//             missionCompleteText.GetComponent<Text>().text = "Mission Complete";
//             audioSource.PlayOneShot(missionCompleteSound);
//             Invoke("ToTitle", 3);
//         }
//     }
//     //ミッション3へ遷移
//     void ToTitle()
// 	{
// 		fadeController.StartFadeOut();
// 		fadeController.changeSceneName = "Title";
// 	}
// }
