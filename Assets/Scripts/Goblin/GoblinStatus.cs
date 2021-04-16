using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoblinStatus : MonoBehaviour 
{
    private Animator animator;
    private int maxGoblinHp;
    public int currentGoblinHp;
    private int attack = 5;
    public int damage;
    private Vector3 hitEffectPos;

    private bool isDead;


    //HPスライダー管理
    public Slider goblinHPSlider;
    public GameObject hitEffectPrefab;

    //無敵時間作成
    public float mutekiFlag = 0;
    [SerializeField] float m_mutekiTime = 1f;
    float m_mutekiTimer;

    public GameObject HealItemPrefab;


    public GameObject SearchAreaCollider;
    SphereCollider sphereCollider;


    void Start()
    {
        animator = GetComponent<Animator>();
        this.maxGoblinHp = 30;
        this.currentGoblinHp = this.maxGoblinHp;
        this.goblinHPSlider.value = 1;
        isDead = false;

        sphereCollider.enabled = true;
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
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Sword")
        {
            if(mutekiFlag == 0 && !isDead)
            {
                // Debug.Log("無敵");
                mutekiFlag = 1;
                this.damage = 10;
                this.currentGoblinHp -= damage;
                // Debug.Log("ダメージ10");
                animator.SetTrigger("Damage");
                goblinHPSlider.value = (float)currentGoblinHp / maxGoblinHp;
                this.hitEffectPos = this.transform.position;
                this.hitEffectPos.y += 1.8f;
                this.transform.position = this.hitEffectPos;
                GameObject hitEffect = Instantiate(hitEffectPrefab, this.hitEffectPos, Quaternion.identity);
            }
        }
        if(this.currentGoblinHp <= 0 && isDead == false)
        {
            animator.SetTrigger("Dead");
            Invoke("DestroyGoblin", 3);
            isDead = true;

            //ここでサーチコライダーをオフにしたい。
            sphereCollider.enabled = false;
        }
    }
    public void DestroyGoblin()
    {
        this.gameObject.SetActive(false);
        var goblinDeathCount = GameObject.Find("GoblinCountText");
        goblinDeathCount.GetComponent<GoblinDeathCount>().CountGoblin(1);
        GameObject healItem = Instantiate(HealItemPrefab, this.transform.position, Quaternion.identity);
    }
}