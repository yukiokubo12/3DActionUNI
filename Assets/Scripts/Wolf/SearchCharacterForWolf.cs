using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchCharacterForWolf : MonoBehaviour
{
    private MoveWolf moveWolf;
    Animator animator;
 
    void Start() 
    {
        animator = GetComponent<Animator>();
        moveWolf = GetComponentInParent<MoveWolf>();
    }
 
    void OnTriggerStay(Collider col) 
    {
        //プレイヤーを発見、追いかける
        if (col.tag == "Player") 
        {
            //敵（自分）の状態を取得
            MoveWolf.WolfState state = moveWolf.GetState();
            //敵（自分）が追いかける状態でなければ追いかける設定に変更
            if (state == MoveWolf.WolfState.Wait || state == MoveWolf.WolfState.Walk) 
            {
                moveWolf.SetState(MoveWolf.WolfState.Chase, col.transform);
            }
        }
    }
 
    //コライダー外でプレイヤー見失う
    void OnTriggerExit(Collider col) 
    {
        if (col.tag == "Player") 
        {
            moveWolf.SetState(MoveWolf.WolfState.Wait);
        }
    }
}
