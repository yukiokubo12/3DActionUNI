using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItemController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
	{
        //プレイヤーとの衝突で回復アイテム消去
        if(other.gameObject.tag == "Player")
        {
            var characterStatus = GameObject.Find("Player");
            characterStatus.GetComponent<CharacterStatus>().Heal(10);
            Destroy(this.gameObject);
        }
    }
}
