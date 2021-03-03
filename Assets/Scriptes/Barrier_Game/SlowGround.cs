using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SlowGround : MonoBehaviour , IPunObservable
{
   [SerializeField] private float slow;
   [SerializeField] private float slow_Bots;
   [SerializeField] private float slowTime;
   
   private void OnTriggerStay (Collider other)
   {
      if (other.GetComponent<PlayerController>())
      {
         other.GetComponent<PlayerController>().SpeedMin(slow,slowTime);
         
      }
      else if (other.GetComponent<BotsControl>())
      {
         other.GetComponent<BotsControl>().SpeedMin(slow_Bots,slowTime);
      }
   }

   
   public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
   {
      if (stream.IsWriting)
      {
         stream.SendNext(slow);
         stream.SendNext(slowTime);
            

      }
      else if (stream.IsReading)
      {
         slowTime = (float) stream.ReceiveNext();
         slow = (float) stream.ReceiveNext();
      }
   }
}
