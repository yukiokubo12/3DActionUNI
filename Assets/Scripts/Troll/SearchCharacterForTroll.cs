using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchCharacterForTroll : MonoBehaviour
{
    private MoveTroll moveTroll;
    Animator animator;
 
    void Start() 
    {
        animator = GetComponent<Animator>();
        moveTroll = GetComponentInParent<MoveTroll>();
    }
 
    void OnTriggerStay(Collider col) 
    {
        //プレイヤーを発見で追いかける
        if (col.tag == "Player") 
        {
            //敵（自分）の状態を取得
            MoveTroll.TrollState state = moveTroll.GetState();
            //敵（自分）が追いかける状態でなければ追いかける設定に変更
            if (state == MoveTroll.TrollState.Wait || state == MoveTroll.TrollState.Walk) 
            {
                moveTroll.SetState(MoveTroll.TrollState.Chase, col.transform);
            }
        }
    }
 
    //コライダー外でプレイヤー見失う
    void OnTriggerExit(Collider col) 
    {
        if (col.tag == "Player") 
        {
            moveTroll.SetState(MoveTroll.TrollState.Wait);
        }
    }
}
