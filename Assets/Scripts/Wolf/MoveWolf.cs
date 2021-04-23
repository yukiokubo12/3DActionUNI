using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWolf : MonoBehaviour
{
    public enum WolfState 
    {
        Walk,
        Wait,
        Chase,
        Attack,
        Damage,
        Dead,
        Freeze
    };
 
    private CharacterController wolfController;
    private Animator animator;
    //目的地
    private Vector3 destination;
    //歩く走るスピード
    [SerializeField]
    private float walkSpeed = 1.0f;
    [SerializeField]
    private float runSpeed = 4.0f;
    //速度
    private Vector3 velocity;
    //移動方向
    private Vector3 direction;
    //到着フラグ
    private bool arrived;
    //setWolfPositionスクリプト
    private SetWolfPosition setWolfPosition;
    //待ち時間
    [SerializeField]
    private float waitTime = 2.5f;
    //経過時間
    private float elapsedTime;
    //敵の状態
    private WolfState state;
    //プレイヤーTransform
    private Transform playerTransform;
    
    [SerializeField] private float freezeTime = 0.5f;

    WolfStatus wolfStatus;
    //無敵状態オンオフ用フラグ
    private float mutekiFlag = 0;

    //攻撃時間制限
    [SerializeField] private float attackTime = 1f;

    public GameObject wolfFace;
 
    void Start() 
    {
        wolfController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        setWolfPosition = GetComponent<SetWolfPosition>();
        setWolfPosition.CreateRandomPosition();
        velocity = Vector3.zero;
        arrived = false;
        elapsedTime = 0f;
        SetState(WolfState.Walk);
    }
 
    void Update () 
    {
        //見回りまたはキャラクターを追いかける状態
        if (state == WolfState.Walk || state == WolfState.Chase) 
        {
            //キャラクターを追いかける状態であればキャラクターの目的地を再設定
            if (state == WolfState.Chase) 
            {
                setWolfPosition.SetDestination (playerTransform.position);
            }
            if (wolfController.isGrounded) 
            {
                velocity = Vector3.zero;
                direction = (setWolfPosition.GetDestination () - transform.position).normalized;
                transform.LookAt (new Vector3 (setWolfPosition.GetDestination ().x, transform.position.y, setWolfPosition.GetDestination ().z));
                if(state == WolfState.Walk)
                {
                    velocity = direction * walkSpeed;
                }
                else
                {
                    velocity = direction * runSpeed;
                }
            }
    
            if (state == WolfState.Walk) 
            {
                //目的地に到着したかどうかの判定
                if (Vector3.Distance (transform.position, setWolfPosition.GetDestination ()) < 0.7f) 
                {
                    SetState(WolfState.Wait);
                }
            } 
            else if (state == WolfState.Chase) 
            {
                //攻撃する距離だったら攻撃
                if (Vector3.Distance (transform.position, setWolfPosition.GetDestination ()) < 1.5f) 
                {
                    SetState(WolfState.Attack);
                }
		    }
	    } 
        //到着で一定時間待つ
        else if (state == WolfState.Wait) 
        {
            elapsedTime += Time.deltaTime;
    
            //待ち時間を越えたら次の目的地を設定
            if (elapsedTime > waitTime) 
            {
                SetState(WolfState.Walk);
            }		
        } 
        else if(state == WolfState.Attack)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime > attackTime)
            {
                SetState(WolfState.Freeze);
            }
        }
        //攻撃後のフリーズ状態
        else if(state == WolfState.Freeze) 
        {
            elapsedTime += Time.deltaTime;
    
            if (elapsedTime > freezeTime) 
            {
                SetState(WolfState.Walk);
            }
        }
        else if(state == WolfState.Dead)
        {
            if(GetComponent<WolfStatus>().currentWolfHp <= 0f)
            {
                SetState(WolfState.Dead);
            }
        }
        
        velocity.y += Physics.gravity.y * Time.deltaTime;
        wolfController.Move (velocity * Time.deltaTime);
        // Debug.Log(state);
    }
 
    //敵キャラクターの状態変更
    public void SetState(WolfState tempState, Transform targetObj = null) 
    {
        state = tempState;
        if (tempState == WolfState.Walk) 
        {
            arrived = false;
            elapsedTime = 0f;
            setWolfPosition.CreateRandomPosition();
            animator.SetFloat("Speed", 1.0f);
        } 
        else if (tempState == WolfState.Chase) 
        {
            //待機状態から追いかける場合もあるため
            arrived = false;
            //追いかける対象セット
            playerTransform = targetObj;
            animator.SetFloat("Speed", 3.0f);
            velocity = direction * runSpeed;
        } 
        else if (tempState == WolfState.Wait) 
        {
            elapsedTime = 0f;
            arrived = true;
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
        } 
        else if (tempState == WolfState.Attack) 
        {
            elapsedTime = 0f;
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
            animator.SetTrigger("Attack");
        } 
        else if (tempState == WolfState.Freeze) 
        {
            elapsedTime = 0f;
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
        }
        else if(tempState == WolfState.Damage)
        {
            velocity = Vector3.zero;
            animator.ResetTrigger("Attack");
            animator.SetTrigger("Damage");
        }
        else if(tempState == WolfState.Dead)
        {
            velocity = Vector3.zero;
        }
}
    //敵キャラクターの状態取得
    public WolfState GetState() 
    {
        return state;
    }
}
