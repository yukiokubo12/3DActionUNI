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
		Vector3 delta_position = transform.position - prePosition;
		animator.SetFloat("Speed", delta_position.magnitude / Time.deltaTime);
		
		if(attacked && !status.attacking)
		{
			attacked = false;
		}
		animator.SetBool("Attacking", (!attacked && status.attacking));
		
		if(!isDown && status.died)
		{
			isDown = true;
			animator.SetTrigger("Down");
		}
		
		prePosition = transform.position;

		if(characterController.isGrounded)
		{
			animator.SetFloat("Speed", Input.GetAxis("Vertical"));
			animator.SetFloat("Speed", Input.GetAxis("Horizontal"));
		}
	}
}