using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchCharacterForGoblin : MonoBehaviour
{
    private MoveGoblin moveGoblin;
    Animator animator;
 
    void Start() 
    {
        animator = GetComponent<Animator>();
        moveGoblin = GetComponentInParent<MoveGoblin>();
    }
 
    void OnTriggerStay(Collider col) 
    {
        //プレイヤーを発見
        if (col.tag == "Player") 
        {
            //敵（自分）の状態を取得
            MoveGoblin.GoblinState state = moveGoblin.GetState();
            //敵（自分）が追いかける状態でなければ追いかける設定に変更
            if (state == MoveGoblin.GoblinState.Wait || state == MoveGoblin.GoblinState.Walk) 
            {
                moveGoblin.SetState(MoveGoblin.GoblinState.Chase, col.transform);
            }
        }
    }
 
    //コライダー外でプレイヤー見失う
    void OnTriggerExit(Collider col) 
    {
        if (col.tag == "Player") 
        {
            moveGoblin.SetState(MoveGoblin.GoblinState.Wait);
        }
    }
}