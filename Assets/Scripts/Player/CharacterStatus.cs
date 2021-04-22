using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class CharacterStatus : MonoBehaviour 
{	
	Animator animator;
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
	public GameObject hitEffectPrefab;

	//エフェクト位置管理
	private Vector3 hitEffectPos;

	public bool isDead;

	public GameObject gameOverText;

	public TimerScript timerScript;
  public FadeController fadeController;

	AudioSource audioSource;
	public AudioClip goblinDamageSound; 
	public AudioClip wolfDamageSound; 
	public AudioClip gameOverSound; 

	void Start()
	{
			animator = GetComponent<Animator>();
			this.maxPlayerHp = 30;
			this.currentPlayerHp = this.maxPlayerHp;
			this.playerHPSlider.value = 1;
			isDead = false;
			audioSource = GetComponent<AudioSource>();
	}

	void Update()
	{
			//無敵状態処理
			if(mutekiFlag == 1)
			{
					m_mutekiTimer += Time.deltaTime;
					if(m_mutekiTimer > m_mutekiTime)
					{
							// Debug.Log("無敵解除");
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


	void HitGoblinArm()
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
				audioSource.PlayOneShot(goblinDamageSound);
			}
	}
	void HitWolfFace()
	{
		if(mutekiFlag == 0 && !isDead)
			{
				mutekiFlag = 1;
				this.damage = 3;
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
