using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Car_Paritical : MonoBehaviour
{
   [SerializeField] private ParticleSystem _particleSystem;
   [SerializeField] private GameObject car;
   

   
   public void parcticalON()
   {
      _particleSystem.Play();
      Invoke("setActive", 1f);
            
      
   }

   public void setActive()
   {
      car.SetActive(false);
      Destroy(this.gameObject,1f);
   }
}
