using UnityEngine;

public class CharaAnimation : MonoBehaviour
{
	Animator animator;
	CharacterStatus status;
	Vector3 prePosition;
	CharacterController characterController;
	GameObject player;
	PlayerCtrl playerCtrl;
	
	void Start ()
	{
		animator = GetComponent<Animator>();
		status = GetComponent<CharacterStatus>();
		characterController = GetComponent<CharacterController>();
		prePosition = transform.position;
	}
	
	//プレイヤー歩行用
	void Update ()
	{
			Vector3 delta_position = transform.position - prePosition;
			float speedAnimeValue = delta_position.magnitude * 5000.0f;
			Vector3 horizontalVelocity = characterController.velocity;
			animator.SetFloat("Speed", horizontalVelocity.magnitude);
	}
}