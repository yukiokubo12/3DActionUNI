using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessPlayerAnimEvent : MonoBehaviour
{
    [SerializeField] Collider m_weapon = null;

    void Start() 
    {
        m_weapon.enabled = false;
    }
 
    public void AttackStart() 
    {
        m_weapon.enabled = true;
    }
 
    public void AttackEnd() 
    {
        m_weapon.enabled = false;
    }
}
