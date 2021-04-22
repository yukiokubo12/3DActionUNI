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

    private MyState state;
    private Vector3 velocity;
    private Animator animator;


    const float RayCastMaxDistance = 100.0f;
    InputManager inputManager;
    [SerializeField] float m_walkSpeed = 3f;
    [SerializeField] float m_rotateSpeed = 10f;
    CharacterController characterController= null;
    float m_verticalVelocity = 0f;

    // [SerializeField] private float jumpPower = 5f;
    private Vector3 moveDirection;

    //走る処理
    [SerializeField] float m_runSpeed = 6.0f;
    private bool runFlag = false;

    // AttackWolf attackWolf;

    public GameObject playerSword;

    private float elapsedTime;
    [SerializeField] private float attackTime = 1f;

    // AudioSource audioSource;
	// public AudioClip runSound;

    void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        velocity = Vector3.zero;

        elapsedTime = 0;

        // audioSource = GetComponent<AudioSource>();
    }

    void LateUpdate()
    {
        animator.SetBool("Jump", false);
        // animator.SetBool("Attack", false);
        state = MyState.Idle;
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
        //攻撃の場合
        else if(state == MyState.Attack0)
        {
            // Attacking0();
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

        // else if(state == MyState.Attack0)
        // {
        //     elapsedTime = 0f;
        //     animator.SetFloat("Speed", 0f);
        //     animator.SetBool("Attack", true);
        //     state = MyState.Attack0;
        // }
        // else if(state == MyState.Idle)
        // {
        //     elapsedTime = 0f;
        //     animator.SetFloat("Speed", 0f);
        // }
    }
    void Walking()
    {
        if (inputManager.Clicked())
        {
            Vector2 clickPos = inputManager.GetCursorPosition();
            // RayCastで対象物を調べる.
            Ray ray = Camera.main.ScreenPointToRay(clickPos);
            RaycastHit hitInfo;

            // if (Physics.Raycast(ray, out hitInfo, RayCastMaxDistance, 1 << LayerMask.NameToLayer("Ground")))
            // {
            //     SendMessage("SetDestination", hitInfo.point);
            // }
        }
    }

    public void Jumping()
    {
        if(inputManager.JumpButton())
        {
            animator.SetTrigger("Jump");
            state = MyState.Jump;
            // velocity.y = jumpPower;
        }
    }
    public void Running()
    {
        if(inputManager.RunButton())
        {
        state = MyState.Run;
        runFlag = true;
        m_walkSpeed = 6.0f;
        animator.SetFloat("Speed", 6.0f);
        // audioSource.PlayOneShot(runSound);
        }
        else
        {
            runFlag = false;
            m_walkSpeed = 3.0f;
        }
    }

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

    public void TakeDamage(Transform enemyTransform) 
    {
        // if(AttackWolf.m_anim.SetTrigger("AttackTrigger"))
        // {
            state = MyState.Damage;
            velocity = Vector3.zero;
            animator.SetTrigger("Damage");
        // }
    //	characterController.Move (enemyTransform.forward * 0.5f);
    }
}

