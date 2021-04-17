using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessCharaAnimEvent : MonoBehaviour
{
    private PlayerCtrl playerCtrl;
    [SerializeField] private CapsuleCollider capsuleCollider;

    private void Start()
    {
        playerCtrl = GetComponent<PlayerCtrl>();
    }
    void AttackStart()
    {
        capsuleCollider.enabled = true;
    }
    void AttackEnd()
    {
        capsuleCollider.enabled = false;
    }
}
