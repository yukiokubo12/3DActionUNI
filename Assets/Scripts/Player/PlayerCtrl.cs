using UnityEngine;
using System.Collections;

public class PlayerCtrl : MonoBehaviour
{
    public enum MyState 
    {
	Normal,
	Damage
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

    [SerializeField] private float jumpPower = 5f;

    //走る処理
    [SerializeField] float m_runSpeed = 6.0f;
    private bool runFlag = false;

    // Use this for initialization
    void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Walking();
        Jumping();
        // AttackStanby();
        Attacking0();

        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        // 入力した方向をカメラを基準とした XZ 平面に変換する
        Vector3 moveDirection = Vector3.forward * v + Vector3.right * h;
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        moveDirection.y = 0f;
        moveDirection = moveDirection.normalized * m_walkSpeed;

        // 入力した方向を向く
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * m_rotateSpeed);
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
        characterController.Move(moveDirection * Time.deltaTime);
        Running();
    }

    void Walking()
    {
        if (inputManager.Clicked())
        {
            Vector2 clickPos = inputManager.GetCursorPosition();
            // RayCastで対象物を調べる.
            Ray ray = Camera.main.ScreenPointToRay(clickPos);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, RayCastMaxDistance, 1 << LayerMask.NameToLayer("Ground")))
            {
                SendMessage("SetDestination", hitInfo.point);
            }
        }
    }

    public void Jumping()
    {
        if(inputManager.JumpButton())
        {
            animator.SetBool("Jump", true);
            Debug.Log(inputManager.JumpButton());
        }
    }
    public void Running()
    {
        if(inputManager.RunButton())
        {
        runFlag = true;
        m_walkSpeed = 6.0f;
        animator.SetFloat("Speed", 6.0f);
        }
        else
        {
            runFlag = false;
            m_walkSpeed = 3.0f;
        }
    }
    
    // public void AttackStanby()
    // {
    //     if(inputManager.AttackStanbyButton())
    //     {
    //         animator.SetBool("AttackStanby", true);
    //     }
    // }

    public void Attacking0()
    {
        if(inputManager.Attack0Button())
        {
            animator.SetBool("Attack0", true);
        }
        else
        {
            animator.SetBool("Attack0", false);
        }
    }

    public void TakeDamage(Transform enemyTransform) 
    {
    state = MyState.Damage;
    velocity = Vector3.zero;
    animator.SetTrigger("Damage");
    //	characterController.Move (enemyTransform.forward * 0.5f);
    }
}

