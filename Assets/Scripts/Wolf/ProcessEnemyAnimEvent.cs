using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessEnemyAnimEvent : MonoBehaviour
{
    private MoveEnemy enemy;
    [SerializeField]
    private SphereCollider sphereCollider;
 
    void Start() {
        enemy = GetComponent<MoveEnemy>();
    }
 
    void AttackStart() {
        sphereCollider.enabled = true;
    }
 
    public void AttackEnd() {
        sphereCollider.enabled = false;
    }
 
    public void StateEnd() {
        enemy.SetState(MoveEnemy.EnemyState.Freeze);
    }
 
    public void EndDamage() {
        enemy.SetState(MoveEnemy.EnemyState.Walk);
    }
}
