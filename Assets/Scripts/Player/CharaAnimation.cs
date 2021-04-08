﻿using UnityEngine;

public class CharaAnimation : MonoBehaviour
{
	Animator animator;
	CharacterStatus status;
	Vector3 prePosition;
	bool isDown = false;
	bool attacked = false;

	CharacterController characterController;
	
	public bool IsAttacked()
	{
		return attacked;
	}
	
	void StartAttackHit()
	{
		Debug.Log ("StartAttackHit");
	}
	
	void EndAttackHit()
	{
		Debug.Log ("EndAttackHit");
	}
	
	void EndAttack()
	{
		attacked = true;
	}
	
	void Start ()
	{
		animator = GetComponent<Animator>();
		status = GetComponent<CharacterStatus>();
		characterController = GetComponent<CharacterController>();
		prePosition = transform.position;
	}
	
	void Update ()
	{
		// animator.SetFloat("Speed", delta_position.magnitude / Time.deltaTime);
		Vector3 delta_position = transform.position - prePosition;
		float speedAnimeValue = delta_position.magnitude * 5000.0f;
		// float speedAnimeValue = delta_position.magnitude * 1.0f;
		// animator.SetFloat("Speed", speedAnimeValue);
		Vector3 horizontalVelocity = characterController.velocity;
		animator.SetFloat("Speed", horizontalVelocity.magnitude);
		
		if(attacked && !status.attacking)
		{
			attacked = false;
		}
		animator.SetBool("Attack", (!attacked && status.attacking));
		
		if(!isDown && status.died)
		{
			isDown = true;
			animator.SetTrigger("Damage");
		}
		
		prePosition = transform.position;

		// if(characterController.isGrounded)
		// {
		// 	animator.SetFloat("Speed", Input.GetAxis("Vertical"));
		// 	animator.SetFloat("Speed", Input.GetAxis("Horizontal"));			
		// }
	}
}