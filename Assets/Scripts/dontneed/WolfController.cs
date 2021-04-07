using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class WolfController : MonoBehaviour
{
    private CharacterController wolfController;
	private Animator animator;
	//目的地
	private Vector3 destination;
	//歩くスピード
	[SerializeField]
	private float walkSpeed = 1.0f;
	//速度
	private Vector3 velocity;
	//移動方向
	private Vector3 direction;
	//到着フラグ
	private bool arrived;
	//スタート位置
	private Vector3 startPosition;
 
	// Use this for initialization
	void Start () 
    {
		wolfController = GetComponent <CharacterController> ();
		animator = GetComponent <Animator> ();
		// var randDestination = Random.insideUnitCircle * 1;
		destination = startPosition + new Vector3(this.gameObject.transform.position.x + 1, 0, this.gameObject.transform.position.y + 1);
		// destination = startPosition + new Vector3(randDestination.x, 0, randDestination.y);
		velocity = Vector3.zero;
		arrived = false;
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
		if (!arrived) 
        {
			if (wolfController.isGrounded) 
            {
				velocity = Vector3.zero;
				animator.SetFloat ("Speed", 2.0f);
				direction = (destination - transform.position).normalized;
				transform.LookAt (new Vector3 (destination.x, transform.position.y, destination.z));
				velocity = direction * walkSpeed;
				Debug.Log (destination);
			}
			velocity.y += Physics.gravity.y * Time.deltaTime;
			wolfController.Move (velocity * Time.deltaTime);
		}
	}
}
