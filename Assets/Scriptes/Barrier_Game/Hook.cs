using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
   
   [SerializeField] private float speed;

   [SerializeField] private Transform MainCheck;
   [SerializeField] private Transform check;
   [SerializeField] private bool isCheck;
   
   private void FixedUpdate()
   {
      if (isCheck)
         HookStart();
      else
      {
         HookReturn();
      }
      
   }

   public void HookStart()
   {
      var valueParent = gameObject.transform.position;
      transform.position = Vector3.MoveTowards(gameObject.transform.position, check.position, speed);
      if (transform.position == check.position)
      {
         isCheck = false;
      }

   }

   public void HookReturn()
   {
      transform.position = Vector3.MoveTowards(gameObject.transform.position, MainCheck.position, speed);
      if (transform.position == MainCheck.position)
      {
         isCheck = true;
      }
      
   }

   
}
