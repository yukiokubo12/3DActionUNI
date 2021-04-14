using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessPlayerAnimEvent : MonoBehaviour
{

    void Start() 
    {
        GetComponent<BoxCollider>().enabled = false;
    }
 
    public void AttackStart() 
    {
        GetComponent<BoxCollider>().enabled = true;
    }
 
    public void AttackEnd() 
    {
        GetComponent<BoxCollider>().enabled = false;
    }
}
