using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackGoblin : MonoBehaviour
{
	[SerializeField] Animator m_anim = null;

    void OnTriggerEnter(Collider col) {
		if(col.tag == "Player") {
			Debug.Log("当たり");
			m_anim.SetTrigger("AttackTrigger");
			// col.GetComponent<PlayerCtrl>().TakeDamage(transform.root);
		}
	}
}
