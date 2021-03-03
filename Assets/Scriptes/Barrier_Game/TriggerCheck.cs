using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    [SerializeField] private GameObject [] gameObjects;
    
   private void OnTriggerEnter(Collider other)
   {
       if (other.GetComponent<PlayerController>() || other.GetComponent<BotsControl>())
       {
           for (int i = 0; i < gameObjects.Length; i++)
           {
               if (gameObjects[i].gameObject != null)
                   gameObjects[i].gameObject.SetActive(true);
           }
       }

   }
}
