using UnityEngine;
using System.Collections;

public class PlayerCtrl : MonoBehaviour
{
    public enum MyState 
    {
        Idle,
        Walk,
        Jump,
        Attack0,
        Damage,
        Run,
        Normal
    };

    //プレイヤー移動
    private Vector3 velocity;
    const float RayCastMaxDistance = 100.0f;
    InputManager inputManager;
    [SerializeField] float m_walkSpeed = 3f;
    [SerializeField] float m_rotateSpeed = 10f;
    float m_verticalVelocity = 0f;
    private Vector3 moveDirection;
    //プレイヤー攻撃ポイント（コライダーつけているところ）
    public GameObject playerSword;

    CharacterController characterController = null;
    private MyState state;
    private Animator animator;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        inputManager = FindObjectOfType<InputManager>();
        velocity = Vector3.zero;
    }

    void Update()
    {
        //待機、歩き状態の場合
        if(state == MyState.Walk || state == MyState.Idle)
        {
            Walking();
            Running();
            Jumping();
            Attacking0();
        }
        //ジャンプ状態の場合
        else if(state == MyState.Jump)
        {
            state = MyState.Jump;
        }
        //走る場合
        else if(state == MyState.Run)
        {
            state =  MyState.Run;
        }
        PlayerMove();
    }

    void LateUpdate()
    {
        animator.SetBool("Jump", false);
        state = MyState.Idle;
    }

    //プレイヤー移動
    void PlayerMove()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        // 入力した方向をカメラを基準とした XZ 平面に変換する
        moveDirection = Vector3.forward * v + Vector3.right * h;
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        moveDirection.y = 0f;
        moveDirection = moveDirection.normalized * m_walkSpeed;

        // 入力した方向を向く
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            if(GetComponent<CharacterStatus>().isDead == false)
            {
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * m_rotateSpeed);
            }
        }

        // 重力の処理
        if (characterController.isGrounded)
        {
            m_verticalVelocity = 0f;
        }
        else
        {
            m_verticalVelocity += Physics.gravity.y * Time.deltaTime;
            moveDirection.y = m_verticalVelocity;
        }

        // 移動
        if(GetComponent<CharacterStatus>().isDead)
        {
            moveDirection = Vector3.zero;
        }
        characterController.Move(moveDirection * Time.deltaTime);
    }

    public void SetState(MyState state)
    {
        if(state == MyState.Idle)
        {
            playerSword.GetComponent<ProcessPlayerAnimEvent>().AttackEnd();
        } 
        else if(state == MyState.Attack0)
        {
            Attacking0();
            playerSword.GetComponent<ProcessPlayerAnimEvent>().AttackStart();
        }
        else if(state == MyState.Jump)
        {
            Jumping();
        }
        else if(state == MyState.Run)
        {
            Running();
        }
        else if(state == MyState.Walk)
        {
            Walking();
        }
    }

    //歩いているとき
    void Walking()
    {
        if (inputManager.Clicked())
        {
            Vector2 clickPos = inputManager.GetCursorPosition();
            // RayCastで対象物を調べる.
            Ray ray = Camera.main.ScreenPointToRay(clickPos);
        }
    }

    //ジャンプのとき
    public void Jumping()
    {
        if(inputManager.JumpButton())
        {
            animator.SetTrigger("Jump");
            state = MyState.Jump;
        }
    }

    //走るとき
    public void Running()
    {
        //走るボタンが押されたら、スピードとアニメーション変化
        if(inputManager.RunButton())
        {
        state = MyState.Run;
        m_walkSpeed = 6.0f;
        animator.SetFloat("Speed", 6.0f);
        }
        //それ以外は歩くスピードで
        else
        {
            m_walkSpeed = 3.0f;
        }
    }

    //攻撃のとき
    public void Attacking0()
    {
        if(inputManager.Attack0Button())
        {
            state = MyState.Attack0;
            animator.SetBool("Attack0", true);
            playerSword.GetComponent<ProcessPlayerAnimEvent>().AttackStart();
        }
        else
        {
            animator.SetBool("Attack0", false);
        }
    }
}
