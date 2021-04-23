using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppeaScript : MonoBehaviour 
{
	//出現させる敵を入れておく
	[SerializeField] GameObject[] enemys;
	//次に敵が出現するまでの時間
	[SerializeField] float appearNextTime;
	//この場所から出現する敵の数
	[SerializeField] int maxNumOfEnemys;
	//今何人の敵を出現させたか（総数）
	private int numberOfEnemys;
	//待ち時間計測
	private float elapsedTime;
    //ゴブリン追加
    public GameObject Goblin;

	void Start () 
    {
		numberOfEnemys = 0;
		elapsedTime = 0f;
	}
 
    void Update () 
    {
        //出現する最大数を超えてたら出現しない
        if (numberOfEnemys >= maxNumOfEnemys) 
        {
            return;
        }
        //経過時間を足す
        elapsedTime += Time.deltaTime;
    
        //経過時間が経ったら
        if (elapsedTime > appearNextTime) 
        {
            elapsedTime = 0f;
            AppearEnemy ();
        }
    }

    //敵出現
    void AppearEnemy() 
    {
	//出現させる敵ランダムに選ぶ
	var randomValue = Random.Range (0, enemys.Length);
	//敵の向きランダムに決定
	var randomRotationY = Random.value * 360f;
    //ゴブリン生成
	GameObject.Instantiate (Goblin, transform.position, Quaternion.Euler (0f, randomRotationY, 0f));
	numberOfEnemys++;
	elapsedTime = 0f;
    }
}
