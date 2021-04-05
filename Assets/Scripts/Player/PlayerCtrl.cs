using UnityEngine;
using System.Collections;

public class PlayerCtrl : MonoBehaviour {
	const float RayCastMaxDistance = 100.0f;
	InputManager inputManager;

	[SerializeField] float m_walkSpeed = 3f;
	[SerializeField] float m_rotateSpeed = 10f;
	CharacterController m_cc = null;
	float m_verticalVelocity = 0f;
	// Use this for initialization
	void Start () 
	{
		inputManager = FindObjectOfType<InputManager>();

		Destroy(GetComponent<CharacterMove>());
		Destroy(GetComponent<Rigidbody>());
		Destroy(GetComponent<CapsuleCollider>());
		m_cc = GetComponent<CharacterController>();

	}
	
	// Update is called once per frame
	void Update () 
	{
		Walking();

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
		if (m_cc.isGrounded)
		{
				m_verticalVelocity = 0f;
		}
		else
		{
				m_verticalVelocity += Physics.gravity.y * Time.deltaTime;
				moveDirection.y = m_verticalVelocity;
		}

		// 移動
		m_cc.Move(moveDirection * Time.deltaTime);
	}
	
	
	
	void Walking()
	{
		if (inputManager.Clicked()) {
			Vector2 clickPos = inputManager.GetCursorPosition();
			// RayCastで対象物を調べる.
			Ray ray = Camera.main.ScreenPointToRay(clickPos);
			RaycastHit hitInfo;
			if(Physics.Raycast(ray,out hitInfo,RayCastMaxDistance,1 << LayerMask.NameToLayer("Ground"))) {
				SendMessage("SetDestination",hitInfo.point);
			}
		}
	}
}
