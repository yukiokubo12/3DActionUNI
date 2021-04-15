using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessGoblinAnimEvent : MonoBehaviour
{
    [SerializeField] Collider m_goblinhand = null;

    void Start() 
    {
        m_goblinhand.enabled = false;
    }
 
    public void AttackStart() 
    {
        m_goblinhand.enabled = true;
    }
 
    public void AttackEnd() 
    {
        m_goblinhand.enabled = false;
    }





    // void Start() 
    // {
    //     GetComponent<CapsuleCollider>().enabled = false;
    // }
 
    // public void AttackStart() 
    // {
    //     GetComponent<CapsuleCollider>().enabled = true;
    // }
 
    // public void AttackEnd() 
    // {
    //     GetComponent<CapsuleCollider>().enabled = false;
    // }



    //public void StateEnd() 
    //{
    //    goblin.SetState(MoveGoblin.GoblinState.Freeze);
    //}
 
    //public void EndDamage() 
    //{
    //    goblin.SetState(MoveGoblin.GoblinState.Walk);
    //}
}