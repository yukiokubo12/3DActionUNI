using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterStatus : MonoBehaviour 
{	
	Animator animator;
	// 体力.
	// public int HP = 100;
	public int maxPlayerHp = 100;
	private int currentPlayerHp;
	
	// 攻撃力.
	// public int power = 10;

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


	void Start()
	{
			animator = GetComponent<Animator>();
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
							// Debug.Log("無敵解除");
							m_mutekiTimer = 0f;
							mutekiFlag = 0;
					}
			}
			Heal(0);
	}
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
			if(mutekiFlag == 0 && !isDead)
			{
				// Debug.Log("無敵");
				mutekiFlag = 1;
				this.damage = 5;
				this.currentPlayerHp -= damage;
				// Debug.Log("ダメージ5");
				animator.SetTrigger("Damage");

				playerHPSlider.value = (float)currentPlayerHp / maxPlayerHp;
				this.hitEffectPos = this.transform.position;
				this.hitEffectPos.y += 1.8f;
				this.transform.position = this.hitEffectPos;
				GameObject hitEffect = Instantiate(hitEffectPrefab, this.hitEffectPos, Quaternion.identity);
			}
		}
			else if(this.currentPlayerHp <= 0 && isDead == false)
			{
				animator.SetTrigger("Death");
				isDead = true;
				gameOverText.GetComponent<Text>().text = "Game Over";
			}
	}
}
