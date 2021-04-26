using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTroll : MonoBehaviour
{
    public enum TrollState 
    {
        Walk,
        Wait,
        Chase,
        Attack,
        Damage,
        Dead,
        Freeze
    };
 
    //目的地
    private Vector3 destination;
    //歩くスピード
    [SerializeField] private float walkSpeed = 0.5f;
    [SerializeField] private float runSpeed = 1.5f;
    //速度
    private Vector3 velocity;
    //移動方向
    private Vector3 direction;
    //待ち時間
    [SerializeField] private float waitTime = 2f;
    //経過時間
    private float elapsedTime;
    //敵の状態
    private TrollState state;
    //プレイヤーTransform
    private Transform playerTransform;
    //止まる時間
    [SerializeField] private float freezeTime = 0.5f;
    //攻撃時間制限
    [SerializeField] private float attackTime = 1f;
    //トロールアタックポイント（コライダーついてるところ）
    public GameObject trollHand;

    private SetTrollPosition setTrollPosition;
    private Animator animator;
    private CharacterController trollController;
    TrollStatus TrollStatus;

    void Start() 
    {
        trollController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        setTrollPosition = GetComponent<SetTrollPosition>();
        setTrollPosition.CreateRandomPosition();
        velocity = Vector3.zero;
        elapsedTime = 0f;
        SetState(TrollState.Walk);
    }
 
    void Update () 
    {
        //見回りまたはキャラクターを追いかける状態
        if (state == TrollState.Walk || state == TrollState.Chase) 
        {
            //キャラクターを追いかける状態であればキャラクターの目的地を再設定
            if (state == TrollState.Chase) 
            {
                setTrollPosition.SetDestination (playerTransform.position);
            }
            //地面ついてる状態
            if (trollController.isGrounded) 
            {
                velocity = Vector3.zero;
                direction = (setTrollPosition.GetDestination () - transform.position).normalized;
                transform.LookAt (new Vector3 (setTrollPosition.GetDestination ().x, transform.position.y, setTrollPosition.GetDestination ().z));
                if(state == TrollState.Walk)
                {
                    velocity = direction * walkSpeed;
                }
                else
                {
                    velocity = direction * runSpeed;
                }
            }
    
            if (state == TrollState.Walk) 
            {
                //目的地に到着したかどうかの判定
                if (Vector3.Distance (transform.position, setTrollPosition.GetDestination ()) < 0.7f) 
                {
                    SetState(TrollState.Wait);
                }
            } 
            else if (state == TrollState.Chase) 
            {
                //攻撃する距離だったら攻撃
                if (Vector3.Distance (transform.position, setTrollPosition.GetDestination ()) < 3.0f) 
                {
                    SetState(TrollState.Attack);
                }
		    }
	    } 
        //到着で一定時間待つ
        else if (state == TrollState.Wait) 
        {
            elapsedTime += Time.deltaTime;
    
            //待ち時間を越えたら次の目的地を設定
            if (elapsedTime > waitTime) 
            {
                SetState(TrollState.Walk);
            }		
        } 
        else if(state == TrollState.Attack)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime > attackTime)
            {
                SetState(TrollState.Freeze);
            }
        }
        //攻撃後のフリーズ状態
        else if(state == TrollState.Freeze) 
        {
            elapsedTime += Time.deltaTime;
    
            if (elapsedTime > freezeTime) 
            {
                SetState(TrollState.Walk);
            }
        }
        else if(state == TrollState.Dead)
        {
            if(GetComponent<TrollStatus>().currentTrollHp <= 0f)
            {
                SetState(TrollState.Dead);
            }
        }
        velocity.y += Physics.gravity.y * Time.deltaTime;
        trollController.Move (velocity * Time.deltaTime);
    }
 
    //敵キャラクターの状態変更
    public void SetState(TrollState tempState, Transform targetObj = null) 
    {
        state = tempState;
        if (tempState == TrollState.Walk) 
        {
            // arrived = false;
            elapsedTime = 0f;
            setTrollPosition.CreateRandomPosition();
            animator.SetFloat("Speed", 1.0f);
        } 
        else if (tempState == TrollState.Chase) 
        {
            //追いかける対象セット
            playerTransform = targetObj;
            animator.SetFloat("Speed", 1.0f);
            velocity = direction * runSpeed;
        } 
        else if (tempState == TrollState.Wait) 
        {
            elapsedTime = 0f;
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
        } 
        else if (tempState == TrollState.Attack) 
        {
            elapsedTime = 0f;
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
            animator.SetTrigger("Attack");
        } 
        else if (tempState == TrollState.Freeze) 
        {
            elapsedTime = 0f;
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
        }
        else if(tempState == TrollState.Damage)
        {
            velocity = Vector3.zero;
            animator.ResetTrigger("Attack");
            animator.SetTrigger("Damage");
        }
        else if(tempState == TrollState.Dead)
        {
            velocity = Vector3.zero;
        }
    }
    //敵キャラクターの状態取得
    public TrollState GetState() 
    {
        return state;
    }
}