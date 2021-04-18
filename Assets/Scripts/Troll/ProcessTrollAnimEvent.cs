using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessTrollAnimEvent : MonoBehaviour
{
    //AnimationEventでコライダーのオンオフ制御
    [SerializeField] Collider m_trollhand = null;

    void Start() 
    {
        m_trollhand.enabled = false;
    }
 
    public void AttackStart() 
    {
        m_trollhand.enabled = true;
    }
 
    public void AttackEnd() 
    {
        m_trollhand.enabled = false;
    }
}