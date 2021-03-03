using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour 
{
   private PhotonView pv;
   public List <Transform>  spawn;
   public List<Transform> spawnBots;
   private Transform posit;
   

  
   public Transform spawnPosition(int _ID)
   {
      return posit = spawn[_ID];
   }

   public Transform spawnPositionBots (int id)
   {
      return posit = spawnBots[id];
   }
   
}
