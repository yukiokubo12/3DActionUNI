using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessWolfAnimEvent : MonoBehaviour
{
    //AnimationEventでコライダーのオンオフ制御
    [SerializeField] Collider m_wolfface = null;

    void Start() 
    {
        m_wolfface.enabled = false;
    }
 
    public void AttackStart() 
    {
        m_wolfface.enabled = true;
    }
 
    public void AttackEnd() 
    {
        m_wolfface.enabled = false;
    }
}
