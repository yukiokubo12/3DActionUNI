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

    bool isbattle = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        this.maxGoblinHp = 100;
        this.currentGoblinHp = this.maxGoblinHp;
        this.goblinHPSlider.value = 1;
    }

    void OnTriggerEnter(Collider other)
    {
        isbattle = true;
        if(other.gameObject.tag == "Sword")
        {
            int damage = 10;
            this.currentGoblinHp -= damage;
            goblinHPSlider.value = (float)currentGoblinHp / maxGoblinHp;
            GameObject hitEffect = Instantiate(hitEffectPrefab, this.transform.position, Quaternion.identity);

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


    void Update()
    {

    }
    
}