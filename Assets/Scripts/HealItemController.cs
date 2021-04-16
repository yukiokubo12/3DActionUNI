using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItemController : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
	{
        if(other.gameObject.tag == "Player")
        {
            var characterStatus = GameObject.Find("Player");
            characterStatus.GetComponent<CharacterStatus>().Heal(20);
            Destroy(this.gameObject);
        }
    }
}
