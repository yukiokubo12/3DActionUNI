using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoblinStatus : MonoBehaviour 
{
    private Animator animator;
    private int maxGoblinHp;
    private int currentGoblinHp;
    private int attack = 5;
    private int damage;

    private bool isDead = true;


    //HPスライダー管理
    public Slider goblinHPSlider;
    public GameObject hitEffectPrefab;

    //無敵時間作成
    public float mutekiFlag = 0;
    public float mutekiTime = 3;
    public float timeStep = 1;

    void Start()
    {
        animator = GetComponent<Animator>();
        this.maxGoblinHp = 100;
        this.currentGoblinHp = this.maxGoblinHp;
        this.goblinHPSlider.value = 1;
    }

    void Update()
    {
        //無敵状態処理
        if(mutekiFlag == 1)
        {
            mutekiTime -= timeStep;
            if(mutekiTime < 0)
            {
                mutekiFlag = 0;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Sword")
        {
            if(mutekiFlag == 0)
            {
            mutekiFlag = 1;
            this.damage = 10;
            this.currentGoblinHp -= damage;
            Debug.Log("ダメージ10");
            animator.SetTrigger("Damage");
            goblinHPSlider.value = (float)currentGoblinHp / maxGoblinHp;
            GameObject hitEffect = Instantiate(hitEffectPrefab, this.transform.position, Quaternion.identity);
            }
        }

        if(this.currentGoblinHp <= 0)
        {
            if(isDead)
            {
            animator.SetTrigger("Dead");
            }
            isDead = false;
        }
    }
}