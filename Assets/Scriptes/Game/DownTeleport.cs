using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownTeleport : MonoBehaviour
{
    [SerializeField] private Transform TP;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            other.GetComponent<PlayerController>().Stun_Time(5f);
            other.transform.position = TP.position;
            
        }
    }
}
