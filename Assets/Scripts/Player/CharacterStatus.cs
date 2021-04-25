using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class CharacterStatus : MonoBehaviour 
{	
	// 体力.
	public int maxPlayerHp;
	private int currentPlayerHp;
	private int damage;
	//無敵時間作成
	public float mutekiFlag = 0;
	[SerializeField] float m_mutekiTime = 1f;
	float m_mutekiTimer;
	//HPスライダー管理
	public Slider playerHPSlider;
	//血エフェクト
	public GameObject hitEffectPrefab;
	public GameObject healEffectPrefab;
	//エフェクト位置管理
	private Vector3 hitEffectPos;
	//死んだかどうか
	public bool isDead;
	//サウンド
	AudioSource audioSource;
	public AudioClip goblinDamageSound; 
	public AudioClip wolfDamageSound; 
	public AudioClip gameOverSound; 

	public GameObject gameOverText;
	public TimerScript timerScript;
  public FadeController fadeController;
	Animator animator;

	void Start()
	{
			animator = GetComponent<Animator>();
			audioSource = GetComponent<AudioSource>();
			this.maxPlayerHp = 50;
			this.currentPlayerHp = this.maxPlayerHp;
			this.playerHPSlider.value = 1;
			isDead = false;
	}

	void Update()
	{
			//無敵状態処理
			if(mutekiFlag == 1)
			{
					m_mutekiTimer += Time.deltaTime;
					if(m_mutekiTimer > m_mutekiTime)
					{
							m_mutekiTimer = 0f;
							mutekiFlag = 0;
					}
			}
			Heal(0);
	}
	
	//回復アイテム拾ったときにHP回復
	public void Heal(int healAmount)
	{
		currentPlayerHp = currentPlayerHp + healAmount;
		playerHPSlider.value = (float)currentPlayerHp / maxPlayerHp;
		if(currentPlayerHp >= maxPlayerHp)
		{
			currentPlayerHp = maxPlayerHp;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "GoblinArm")
		{
			HitGoblinArm();
		}
		if(other.gameObject.tag == "WolfFace")
		{
			HitWolfFace();
		}
		if(other.gameObject.tag == "TrollArm")
		{
			HitTrollArm();
		}
		if(other.gameObject.tag == "HealItem")
		{
			GameObject healEffect = Instantiate(healEffectPrefab, this.transform.position, Quaternion.identity);
		}
		//プレイヤーが死んだらゲームオーバー
		if(this.currentPlayerHp <= 0 && isDead == false || timerScript.totalTime <= 0f)
		{
			animator.SetTrigger("Death");
			isDead = true;
			gameOverText.GetComponent<Text>().text = "Game Over";
			audioSource.PlayOneShot(gameOverSound);
			Invoke("ToTitleScene", 3);
		}
	}

	void ToTitleScene()
	{
		fadeController.StartFadeOut();
		fadeController.changeSceneName = "Title";
	}

	//ゴブリンの攻撃受けたとき
	void HitGoblinArm()
	{
		if(mutekiFlag == 0 && !isDead)
			{
				mutekiFlag = 1;
				this.damage = 8;
				this.currentPlayerHp -= damage;
				animator.SetTrigger("Damage");
				playerHPSlider.value = (float)currentPlayerHp / maxPlayerHp;
				this.hitEffectPos = this.transform.position;
				this.hitEffectPos.y += 1.8f;
				this.transform.position = this.hitEffectPos;
				GameObject hitEffect = Instantiate(hitEffectPrefab, this.hitEffectPos, Quaternion.identity);
				audioSource.PlayOneShot(goblinDamageSound);
			}
	}

	//ウルフの攻撃受けたとき
	void HitWolfFace()
	{
		if(mutekiFlag == 0 && !isDead)
			{
				mutekiFlag = 1;
				this.damage = 5;
				this.currentPlayerHp -= damage;
				animator.SetTrigger("Damage");
				playerHPSlider.value = (float)currentPlayerHp / maxPlayerHp;
				this.hitEffectPos = this.transform.position;
				this.hitEffectPos.y += 1.8f;
				this.transform.position = this.hitEffectPos;
				GameObject hitEffect = Instantiate(hitEffectPrefab, this.hitEffectPos, Quaternion.identity);
				audioSource.PlayOneShot(wolfDamageSound);
			}
	}

	//トロールの攻撃受けたとき
	void HitTrollArm()
	{
		if(mutekiFlag == 0 && !isDead)
			{
				mutekiFlag = 1;
				this.damage = 15;
				this.currentPlayerHp -= damage;
				animator.SetTrigger("Damage");
				playerHPSlider.value = (float)currentPlayerHp / maxPlayerHp;
				this.hitEffectPos = this.transform.position;
				this.hitEffectPos.y += 1.8f;
				this.transform.position = this.hitEffectPos;
				GameObject hitEffect = Instantiate(hitEffectPrefab, this.hitEffectPos, Quaternion.identity);
				audioSource.PlayOneShot(goblinDamageSound);
			}
	}
}
