﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public enum EnemyState 
    {
        Walk,
        Wait,
        Chase,
        Attack,
        Freeze
    };
 
    private CharacterController enemyController;
    private Animator animator;
    //　目的地
    private Vector3 destination;
    //　歩くスピード
    [SerializeField]
    private float walkSpeed = 1.0f;
    [SerializeField]
    private float runSpeed = 2.0f;
    //　速度
    private Vector3 velocity;
    //　移動方向
    private Vector3 direction;
    //　到着フラグ
    private bool arrived;
    //　SetPositionスクリプト
    private SetPosition setPosition;
    //　待ち時間
    [SerializeField]
    private float waitTime = 5f;
    //　経過時間
    private float elapsedTime;
    // 敵の状態
    private EnemyState state;
    //　プレイヤーTransform
    private Transform playerTransform;

    private SearchCharacter searchCharacter;
    [SerializeField] private float freezeTime = 0.5f;
 
    // Use this for initialization
    void Start() 
    {
        enemyController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        setPosition = GetComponent<SetPosition>();
        setPosition.CreateRandomPosition();
        velocity = Vector3.zero;
        arrived = false;
        elapsedTime = 0f;
        SetState(EnemyState.Walk);
        searchCharacter = GetComponentInParent<SearchCharacter>();
    }
 
    // Update is called once per frame
    void Update () 
    {
        //　見回りまたはキャラクターを追いかける状態
        if (state == EnemyState.Walk || state == EnemyState.Chase) 
        {
            //　キャラクターを追いかける状態であればキャラクターの目的地を再設定
            if (state == EnemyState.Chase) 
            {
                setPosition.SetDestination (playerTransform.position);
            }
            if (enemyController.isGrounded) 
            {
                velocity = Vector3.zero;
                //animator.SetFloat ("Speed", 2.0f);
                direction = (setPosition.GetDestination () - transform.position).normalized;
                transform.LookAt (new Vector3 (setPosition.GetDestination ().x, transform.position.y, setPosition.GetDestination ().z));
                // velocity = direction * walkSpeed;
                if(state == EnemyState.Walk)
                {
                    velocity = direction * walkSpeed;
                }
                else
                {
                    velocity = direction * runSpeed;
                }
            }
    
            if (state == EnemyState.Walk) 
            {
                //　目的地に到着したかどうかの判定
                if (Vector3.Distance (transform.position, setPosition.GetDestination ()) < 0.7f) 
                {
                    SetState(EnemyState.Wait);
                    //animator.SetFloat ("Speed", 0.0f);
                }
            } 
            else if (state == EnemyState.Chase) 
            {
                //　攻撃する距離だったら攻撃
                if (Vector3.Distance (transform.position, setPosition.GetDestination ()) < 1f) 
                {
                    SetState(EnemyState.Attack);
                }
		    }
	    } 
        //到着で一定時間待つ
        else if (state == EnemyState.Wait) 
        {
            elapsedTime += Time.deltaTime;
    
            //　待ち時間を越えたら次の目的地を設定
            if (elapsedTime > waitTime) 
            {
                SetState(EnemyState.Walk);
            }		
        } 
        //　攻撃後のフリーズ状態
        else if(state == EnemyState.Freeze) 
        {
            elapsedTime += Time.deltaTime;
    
            if (elapsedTime > freezeTime) 
            {
                SetState(EnemyState.Walk);
            }
        }
        velocity.y += Physics.gravity.y * Time.deltaTime;
        enemyController.Move (velocity * Time.deltaTime);
    }
 
    //　敵キャラクターの状態変更メソッド
    public void SetState(EnemyState tempState, Transform targetObj = null) 
    {
        state = tempState;
        if (tempState == EnemyState.Walk) 
        {
            arrived = false;
            elapsedTime = 0f;
            setPosition.CreateRandomPosition();
            animator.SetFloat("Speed", 1.0f);
        } 
        else if (tempState == EnemyState.Chase) 
        {
            //　待機状態から追いかける場合もあるのでOff
            arrived = false;
            //　追いかける対象をセット
            playerTransform = targetObj;
            animator.SetFloat("Speed", 3.0f);
            velocity = direction * runSpeed;
        } 
        else if (tempState == EnemyState.Wait) 
        {
            elapsedTime = 0f;
            arrived = true;
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
        } 
        else if (tempState == EnemyState.Attack) 
        {
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
            animator.SetBool("Attack", true);
        } 
        else if (tempState == EnemyState.Freeze) 
        {
            elapsedTime = 0f;
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
            animator.SetBool("Attack", false);
    }
}
    //　敵キャラクターの状態取得メソッド
    public EnemyState GetState() 
    {
        return state;
    }
}



// {
//     public enum GoblinState 
//     {
//         Walk,
//         Wait,
//         Chase,
//         Attack,
//         Damage,
//         Freeze
//     };
 
//     private CharacterController goblinController;
//     private Animator animator;
//     //目的地
//     private Vector3 destination;
//     //歩くスピード
//     [SerializeField]
//     private float walkSpeed = 1.0f;
//     [SerializeField]
//     private float runSpeed = 2.0f;
//     //速度
//     private Vector3 velocity;
//     //移動方向
//     private Vector3 direction;
//     //到着フラグ
//     private bool arrived;
//     //setGoblinPositionスクリプト
//     private SetGoblinPosition setGoblinPosition;
//     //待ち時間
//     [SerializeField]
//     private float waitTime = 5f;
//     //経過時間
//     private float elapsedTime;
//     //敵の状態
//     private GoblinState state;
//     //プレイヤーTransform
//     private Transform playerTransform;
    
//     private SearchCharacter searchCharacter;
//     [SerializeField] private float freezeTime = 0.5f;

//     GoblinStatus goblinStatus;
//     //無敵状態オンオフ用フラグ
//     private float mutekiFlag = 0;

//     //攻撃時間制限
//     [SerializeField] private float attackTime = 1f;

//     public GameObject goblinHand;


//     public bool isMove = true;

 
//     void Start() 
//     {
//         goblinController = GetComponent<CharacterController>();
//         animator = GetComponent<Animator>();
//         setGoblinPosition = GetComponent<SetGoblinPosition>();
//         setGoblinPosition.CreateRandomPosition();
//         velocity = Vector3.zero;
//         arrived = false;
//         elapsedTime = 0f;
//         SetState(GoblinState.Walk);
//         searchCharacter = GetComponentInParent<SearchCharacter>();
//     }
 
//     void Update () 
//     {
//         //見回りまたはキャラクターを追いかける状態
//         if (state == GoblinState.Walk || state == GoblinState.Chase) 
//         {
//             isMove = true;
//             //キャラクターを追いかける状態であればキャラクターの目的地を再設定
//             if (state == GoblinState.Chase) 
//             {
//                 // isMove = true;
//                 setGoblinPosition.SetDestination (playerTransform.position);
//             }
//             if (goblinController.isGrounded) 
//             {
//                 velocity = Vector3.zero;
//                 direction = (setGoblinPosition.GetDestination () - transform.position).normalized;
//                 transform.LookAt (new Vector3 (setGoblinPosition.GetDestination ().x, transform.position.y, setGoblinPosition.GetDestination ().z));
//                 if(state == GoblinState.Walk)
//                 {
//                     velocity = direction * walkSpeed;
//                 }
//                 else
//                 {
//                     velocity = direction * runSpeed;
//                 }
//             }
    
//             if (state == GoblinState.Walk) 
//             {
//                 //目的地に到着したかどうかの判定
//                 if (Vector3.Distance (transform.position, setGoblinPosition.GetDestination ()) < 0.7f) 
//                 {
//                     SetState(GoblinState.Wait);
//                 }
//             } 
//             else if (state == GoblinState.Chase) 
//             {
//                 //攻撃する距離だったら攻撃
//                 if (Vector3.Distance (transform.position, setGoblinPosition.GetDestination ()) < 1.4f) 
//                 {
//                     SetState(GoblinState.Attack);
//                 }
// 		    }
// 	    } 
//         //到着で一定時間待つ
//         else if (state == GoblinState.Wait) 
//         {
//             elapsedTime += Time.deltaTime;
    
//             //待ち時間を越えたら次の目的地を設定
//             if (elapsedTime > waitTime) 
//             {
//                 SetState(GoblinState.Walk);
//             }		
//         } 
//         else if(state == GoblinState.Attack)
//         {
//             elapsedTime += Time.deltaTime;
//             if(elapsedTime > attackTime)
//             {
//                 SetState(GoblinState.Freeze);
//             }
//         }
//         //攻撃後のフリーズ状態
//         else if(state == GoblinState.Freeze) 
//         {
//             elapsedTime += Time.deltaTime;
    
//             if (elapsedTime > freezeTime) 
//             {
//                 SetState(GoblinState.Walk);
//             }
//         }
        
//         velocity.y += Physics.gravity.y * Time.deltaTime;
//         goblinController.Move (velocity * Time.deltaTime);
//     }
 
//     //敵キャラクターの状態変更
//     public void SetState(GoblinState tempState, Transform targetObj = null) 
//     {
//         state = tempState;
//         if (tempState == GoblinState.Walk) 
//         {
//             arrived = false;
//             elapsedTime = 0f;
//             setGoblinPosition.CreateRandomPosition();
//             animator.SetFloat("Speed", 1.0f);
//         } 
//         else if (tempState == GoblinState.Chase) 
//         {
//             //待機状態から追いかける場合もあるため
//             arrived = false;
//             //追いかける対象セット
//             playerTransform = targetObj;
//             animator.SetFloat("Speed", 3.0f);
//             velocity = direction * runSpeed;
//         } 
//         else if (tempState == GoblinState.Wait) 
//         {
//             elapsedTime = 0f;
//             arrived = true;
//             velocity = Vector3.zero;
//             animator.SetFloat("Speed", 0f);
//         } 
//         else if (tempState == GoblinState.Attack) 
//         {
//             elapsedTime = 0f;
//             velocity = Vector3.zero;
//             animator.SetFloat("Speed", 0f);
//             animator.SetTrigger("Attack");
//         } 
//         else if (tempState == GoblinState.Freeze) 
//         {
//             elapsedTime = 0f;
//             velocity = Vector3.zero;
//             animator.SetFloat("Speed", 0f);
//         }
//         else if(tempState == GoblinState.Damage)
//         {
//             velocity = Vector3.zero;
//             animator.ResetTrigger("Attack");
//             animator.SetTrigger("Damage");
//         }
// }
//     //敵キャラクターの状態取得
//     public GoblinState GetState() 
//     {
//         return state;
//     }
// }
