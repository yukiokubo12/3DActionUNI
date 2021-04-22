﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class RelayTrigger : MonoBehaviour
{
  [SerializeField] GameObject relayTo;
  private FootstepSEPlayer footstepSEPlayer;

  private void Awake()
  {
    footstepSEPlayer = relayTo.GetComponent<FootstepSEPlayer>();
  }

  private void OnTriggerEnter(Collider other)
  {
    footstepSEPlayer.RelayedTrigger(other);
  }

}