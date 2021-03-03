using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceUP : MonoBehaviour
{
   [SerializeField] private int force;

   private void OnTriggerEnter(Collider other)
   {
      if (other.GetComponent<Rigidbody>())
         other.GetComponent<Rigidbody>().AddForce(Vector3.up * force);
   }
}
