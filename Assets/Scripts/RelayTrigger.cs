using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

//Terrainオブジェクトへアタッチ（足音判定用）
public class RelayTrigger : MonoBehaviour
{
  [SerializeField] GameObject relayTo;
  private FootstepSEPlayer footstepSEPlayer;

  private void Awake()
  {
    footstepSEPlayer = relayTo.GetComponent<FootstepSEPlayer>();
  }

  //地面に足がついたかどうか
  private void OnTriggerEnter(Collider other)
  {
    footstepSEPlayer.RelayedTrigger(other);
  }
}
