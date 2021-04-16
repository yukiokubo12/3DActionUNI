using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessGoblinAnimEvent : MonoBehaviour
{
    [SerializeField] Collider m_goblinhand = null;

    void Start() 
    {
        m_goblinhand.enabled = false;
    }
 
    public void AttackStart() 
    {
        m_goblinhand.enabled = true;
    }
 
    public void AttackEnd() 
    {
        m_goblinhand.enabled = false;
    }
}