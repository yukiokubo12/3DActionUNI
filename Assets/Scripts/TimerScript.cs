using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour 
{
	//トータル制限時間
	public float totalTime;
	//制限時間（分）
	[SerializeField] private int minute;
	//制限時間（秒）
	[SerializeField] private float seconds;
	//前回Update時の秒数
	private float oldSeconds;
	//タイマー表示テキスト
	private Text timerText;
	//ゲームオーバーテキスト
	public GameObject gameOverText;
 
	void Start () 
	{
		totalTime = minute * 60 + seconds;
		oldSeconds = 0f;
		timerText = GetComponentInChildren<Text>();
	}
 
	void Update () 
	{
		//制限時間が0秒以下なら何もしない
		if (totalTime <= 0f) 
		{
			return;
		}
		//一旦トータルの制限時間を計測
		totalTime = minute * 60 + seconds;
		totalTime -= Time.deltaTime;
 
		//再設定
		minute = (int) totalTime / 60;
		seconds = totalTime - minute * 60;
 
		//タイマーテキストに時間を表示
		if((int)seconds != (int)oldSeconds) {
			timerText.text = minute.ToString("00") + ":" + ((int) seconds).ToString("00");
		}
		oldSeconds = seconds;
		// if(totalTime <= 0f) 
		// {
		// 	Debug.Log("制限時間終了");
		// }
	}
}
