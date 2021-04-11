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

    void Start()
    {
        animator = GetComponent<Animator>();
        this.maxGoblinHp = 30;
        this.currentGoblinHp = this.maxGoblinHp;
        this.goblinHPSlider.value = 1;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Sword")
        {
            int damage = 30;
            this.currentGoblinHp -= damage;
            goblinHPSlider.value = (float)currentGoblinHp / maxGoblinHp;
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