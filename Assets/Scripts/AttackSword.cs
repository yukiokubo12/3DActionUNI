using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class AttackSword : MonoBehaviour
{
    private bool triggerFlag = true;

    private int enemyid;
    public GoblinStatus goblinStatus;

    void Start()
    {
        goblinStatus = GetComponent<GoblinStatus>();
    }

    // void OnTriggerEnter(Collider col)
    // {
    //     if(col.tag == "Goblin")
    //     {
            // int[] enemyid = new int[3];
            // enemyid[0] = goblinStatus.id;
            // enemyid[1] = goblinStatus.id;
            // enemyid[2] = goblinStatus.id;

            // if(triggerFlag)
            // {
            //     for(int i = 0; i < enemyid.Length; i++)
            //     {
                    // if(enemyid = 100)
                    // {
                    //     return;
                    // }
                    // if(enemyid = 101)
                    // {
                    //     return;
                    // }
                // }
            // Debug.Log("ゴブリンに当たった");
            // col.GetComponent<MoveGoblin>().SetState(MoveGoblin.GoblinState.Damage);
            // }
            // triggerFlag = false;
    //     }
    // }
}
