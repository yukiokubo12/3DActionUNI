using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessGoblinAnimEvent : MonoBehaviour
{
    private MoveGoblin goblin;
    [SerializeField]
    private SphereCollider sphereCollider;
 
    void Start() {
        goblin = GetComponent<MoveGoblin>();
    }
 
    void AttackStart() {
        sphereCollider.enabled = true;
    }
 
    public void AttackEnd() {
        sphereCollider.enabled = false;
    }
 
    public void StateEnd() {
        goblin.SetState(MoveGoblin.GoblinState.Freeze);
    }
 
    public void EndDamage() {
        goblin.SetState(MoveGoblin.GoblinState.Walk);
    }
}