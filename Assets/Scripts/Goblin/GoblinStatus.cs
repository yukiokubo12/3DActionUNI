using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinStatus : MonoBehaviour 
{
    private Animator animator;
    private int maxGoblinHp;
    private int currentGoblinHp;
    private int attack = 5;
    private int damage;

    private bool isDead = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        this.maxGoblinHp = 30;
        this.currentGoblinHp = this.maxGoblinHp;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Sword")
        {
            int damage = 30;
            this.currentGoblinHp -= damage;
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