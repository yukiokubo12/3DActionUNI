using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessGoblinAnimEvent : MonoBehaviour
{
    private MoveGoblin goblin;
    [SerializeField] private CapsuleCollider capsuleCollider;
 
    void Start() 
    {
        goblin = GetComponent<MoveGoblin>();
        capsuleCollider.enabled = false;
    }
 
    void AttackStart() {
        capsuleCollider.enabled = true;
    }
 
    public void AttackEnd() {
        capsuleCollider.enabled = false;
    }
 
    public void StateEnd() {
        goblin.SetState(MoveGoblin.GoblinState.Freeze);
    }
 
    public void EndDamage() {
        goblin.SetState(MoveGoblin.GoblinState.Walk);
    }
}