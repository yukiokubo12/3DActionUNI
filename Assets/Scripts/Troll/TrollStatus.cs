using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrollStatus : MonoBehaviour
{
    //体力
    private int maxTrollHp;
    public int currentTrollHp;
    public int damage;
    //死んでるかどうか
    private bool isDead;
    //HPスライダー管理
    public Slider trollHPSlider;
    //血エフェクト
    public GameObject hitEffectPrefab;
    private Vector3 hitEffectPos;
    //無敵時間作成
    public float mutekiFlag = 0;
    [SerializeField] float m_mutekiTime = 1f;
    float m_mutekiTimer;
    //サウンド
    private AudioSource audioSource;
    public AudioClip attackSound;
    public AudioClip deadSound;
    // public AudioClip vsTrollSound;

    MoveTroll moveTroll;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        moveTroll = GetComponent<MoveTroll>();
        audioSource = GetComponent<AudioSource>();
        this.maxTrollHp = 30;
        this.currentTrollHp = this.maxTrollHp;
        this.trollHPSlider.value = 1;
        isDead = false;
    }

    void Update()
    {
        //無敵状態処理
        if(mutekiFlag == 1)
        {
            m_mutekiTimer += Time.deltaTime;
            //Timerが上回れば無敵タイム終了
            if(m_mutekiTimer > m_mutekiTime)
            {
                m_mutekiTimer = 0f;
                mutekiFlag = 0;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //プレイヤーの剣に当たったらダメージ受ける
        if(other.gameObject.tag == "Sword")
        {
            //無敵時間解除
            if(mutekiFlag == 0 && !isDead)
            {
                mutekiFlag = 1;
                this.damage = 10;
                this.currentTrollHp -= damage;
                animator.SetTrigger("Damage");
                trollHPSlider.value = (float)currentTrollHp / maxTrollHp;
                this.hitEffectPos = this.transform.position;
                this.hitEffectPos.y += 1.8f;
                this.transform.position = this.hitEffectPos;
                GameObject hitEffect = Instantiate(hitEffectPrefab, this.hitEffectPos, Quaternion.identity);
                audioSource.PlayOneShot(attackSound);
            }
        }
        //HPが0以下になったら倒れる
        if(this.currentTrollHp <= 0 && isDead == false)
        {
            animator.SetTrigger("Dead");
            Invoke("DestroyTroll", 3);
            isDead = true;
            moveTroll.SetState(MoveTroll.TrollState.Dead);
            audioSource.PlayOneShot(deadSound);
        }
    }
 
    //ゴブリン消滅
    public void DestroyTroll()
    {
        this.gameObject.SetActive(false);
        var trollDeathCount = GameObject.Find("TrollCountText");
        trollDeathCount.GetComponent<TrollDeathCount>().CountTroll(1);
    }
}
