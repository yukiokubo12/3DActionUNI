using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WolfStatus : MonoBehaviour
{
    private Animator animator;
    //体力
    private int maxWolfHp;
    public int currentWolfHp;
    //攻撃力
    private int attack = 5;

    public int damage;
    //死んでるかどうか
    private bool isDead;

    //HPスライダー管理
    public Slider wolfHPSlider;

    public GameObject hitEffectPrefab;
    private Vector3 hitEffectPos;

    //無敵時間作成
    public float mutekiFlag = 0;
    [SerializeField] float m_mutekiTime = 1.0f;
    float m_mutekiTimer;

    public GameObject HealItemPrefab;

    MoveWolf moveWolf;

    void Start()
    {
        animator = GetComponent<Animator>();
        moveWolf = GetComponent<MoveWolf>();
        this.maxWolfHp = 20;
        this.currentWolfHp = this.maxWolfHp;
        this.wolfHPSlider.value = 1;
        this.isDead = false;
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
        //プレイヤーの剣に当たったら
        if(other.gameObject.tag == "Sword")
        {
            //無敵時間解除
            if(mutekiFlag == 0 && !isDead)
            {
                mutekiFlag = 1;
                this.damage = 10;
                this.currentWolfHp -= damage;
                animator.SetTrigger("Damage");
                wolfHPSlider.value = (float)currentWolfHp / maxWolfHp;
                this.hitEffectPos = this.transform.position;
                this.hitEffectPos.y += 1.8f;
                this.transform.position = this.hitEffectPos;
                GameObject hitEffect = Instantiate(hitEffectPrefab, this.hitEffectPos, Quaternion.identity);
            }
        }
        //HPが0以下になったら
        if(this.currentWolfHp <= 0 && this.isDead == false)
        {
            animator.SetTrigger("Dead");
            Debug.Log("ウルフ死亡アニメーション");
            Invoke("DestroyWolf", 2);
            this.isDead = true;
            moveWolf.SetState(MoveWolf.WolfState.Dead);
        }
    }
 
    //ウルフ消滅させ、アイテム出す
    public void DestroyWolf()
    {
        this.gameObject.SetActive(false);
        var wolfDeathCount = GameObject.Find("WolfCountText");
        // wolfDeathCount.GetComponent<WolfDeathCount>().CountWolf(1);
        if(Random.Range(0, 2) == 0)
        {
            GameObject healItem = Instantiate(HealItemPrefab, this.transform.position, Quaternion.identity);
        }
    }
}
