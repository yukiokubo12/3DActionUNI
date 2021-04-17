using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoblinStatus : MonoBehaviour 
{
    private Animator animator;
    //体力
    private int maxGoblinHp;
    public int currentGoblinHp;
    //攻撃力
    private int attack = 5;

    public int damage;
    //死んでるかどうか
    private bool isDead;

    //HPスライダー管理
    public Slider goblinHPSlider;

    public GameObject hitEffectPrefab;
    private Vector3 hitEffectPos;

    //無敵時間作成
    public float mutekiFlag = 0;
    [SerializeField] float m_mutekiTime = 1f;
    float m_mutekiTimer;

    public GameObject HealItemPrefab;

    MoveGoblin moveGoblin;

    void Start()
    {
        animator = GetComponent<Animator>();
        moveGoblin = GetComponent<MoveGoblin>();
        this.maxGoblinHp = 30;
        this.currentGoblinHp = this.maxGoblinHp;
        this.goblinHPSlider.value = 1;
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
        //プレイヤーの剣に当たったら
        if(other.gameObject.tag == "Sword")
        {
            //無敵時間解除
            if(mutekiFlag == 0 && !isDead)
            {
                mutekiFlag = 1;
                this.damage = 10;
                this.currentGoblinHp -= damage;
                animator.SetTrigger("Damage");
                goblinHPSlider.value = (float)currentGoblinHp / maxGoblinHp;
                this.hitEffectPos = this.transform.position;
                this.hitEffectPos.y += 1.8f;
                this.transform.position = this.hitEffectPos;
                GameObject hitEffect = Instantiate(hitEffectPrefab, this.hitEffectPos, Quaternion.identity);
            }
        }
        //HPが0以下になったら
        if(this.currentGoblinHp <= 0 && isDead == false)
        {
            animator.SetTrigger("Dead");
            Invoke("DestroyGoblin", 3);
            isDead = true;
            moveGoblin.SetState(MoveGoblin.GoblinState.Dead);
        }
    }
 
    //ゴブリン消滅させ、アイテム出す
    public void DestroyGoblin()
    {
        this.gameObject.SetActive(false);
        var goblinDeathCount = GameObject.Find("GoblinCountText");
        goblinDeathCount.GetComponent<GoblinDeathCount>().CountGoblin(1);
        if(Random.Range(0, 2) == 0)
        {
            GameObject healItem = Instantiate(HealItemPrefab, this.transform.position, Quaternion.identity);
        }
    }
}