using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Stun : MonoBehaviour, IPunObservable
{ 
    private PlayerController _playerController;
    [SerializeField] private float stun_Time;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PhotonView>() != null && other.GetComponent<PlayerController>() != null)
        {

            if (other.GetComponent<PlayerController>() && !other.GetComponent<PlayerController>().isShield)
            {
                other.GetComponent<PlayerController>().Stun_Time(stun_Time);

            }
            
        }

        if (other.GetComponent<PhotonView>() != null && other.GetComponent<BotsControl>() != null)
        {
            if (other.GetComponent<BotsControl>() && !other.GetComponent<BotsControl>().isShield)
            {
                other.GetComponent<BotsControl>().Stun_Time(stun_Time);
            }
            
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(stun_Time);
            

        }
        else if (stream.IsReading)
        {
         
            stun_Time = (float) stream.ReceiveNext();
        }
    }
}
