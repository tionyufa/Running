using System;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour 
{
   public Text text;
   public double sec = 0;
   private float min = 0;
   private bool isTime ;

   private void Start()
   {
      isTime = false;
   }

   private void Update()
   {
      TimerGame();    
      
   }

   public void boolTime (bool isT)
   {
      isTime = isT;
   }

   public void testTimer()
   {
     
   }
   private void TimerGame()
   {
      if (isTime)
      {
         sec += Time.deltaTime;
         
         if (sec > 60)
         {
            sec = 0;
            min++;
            return;
         }
         text.text = String.Format("Time: " + min.ToString("")+ ":" + sec.ToString("00"));

      }
   }


   // public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
   // {
   //    if (stream.IsWriting)
   //    {
   //       stream.SendNext(isTime);
   //          
   //
   //    }
   //    else if (stream.IsReading)
   //    {
   //       isTime = (bool) stream.ReceiveNext();
   //    }
   // }
}
