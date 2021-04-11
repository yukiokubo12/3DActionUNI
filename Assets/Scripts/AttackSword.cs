using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSword : MonoBehaviour
{
    private bool triggerFlag = true;

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Goblin")
        {
            if(triggerFlag)
            {
            Debug.Log("ゴブリンに当たった");
            col.GetComponent<MoveGoblin>().SetState(MoveGoblin.GoblinState.Damage);
            }
            triggerFlag = false;
        }
    }
}
