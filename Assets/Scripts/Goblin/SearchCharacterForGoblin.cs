using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchCharacterForGoblin : MonoBehaviour
{
    private MoveGoblin moveGoblin;
    Animator animator;
 
    void Start() {
        animator = GetComponent<Animator>();
        moveGoblin = GetComponentInParent<MoveGoblin>();
    }
 
    void OnTriggerStay(Collider col) 
    {
        //　プレイヤーキャラクターを発見
        if (col.tag == "Player") {
            //　敵キャラクターの状態を取得
            MoveGoblin.GoblinState state = moveGoblin.GetState();
            //　敵キャラクターが追いかける状態でなければ追いかける設定に変更
            if (state == MoveGoblin.GoblinState.Wait || state == MoveGoblin.GoblinState.Walk) 
            {
                // Debug.Log("プレイヤー発見");
                moveGoblin.SetState(MoveGoblin.GoblinState.Chase, col.transform);
                // animator.SetFloat("Run", 3.0f);
                // Debug.Log("プレイヤーへ向かって走る");
            }
        }
    }
 
    void OnTriggerExit(Collider col) {
        if (col.tag == "Player") {
            // Debug.Log("見失う");
            moveGoblin.SetState(MoveGoblin.GoblinState.Wait);
        }
    }
}