using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWolf : MonoBehaviour
{
    void OnTriggerEnter(Collider col) {
		if(col.tag == "Player") {
			Debug.Log("当たり");
			col.GetComponent<PlayerCtrl>().TakeDamage(transform.root);
		}
	}
}
