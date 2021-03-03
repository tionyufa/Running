using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SlowSkill : MonoBehaviour , IPunObservable
{
    public int ID;
    private Quaternion _quaternion;
    private Transform _transform;
    [SerializeField] private float slow;
    [SerializeField] private float slow_Bots;
    [SerializeField] private float slowTime;
    [SerializeField] private Vector3 _vector3;
    private bool _isTransformNotNull;

    private void Start()
    {
        _isTransformNotNull = _transform != null;
    }

    private void Update()
    {
        if (_isTransformNotNull)
        {
            transform.position = _transform.position + _vector3;
            transform.rotation = _quaternion;
        }
    }

    public void setVector(Transform transform,Quaternion _qua)
    {
        _transform = transform;
        _quaternion = _qua;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PhotonView>() != null && other.GetComponent<BotsControl>() != null)
        {
           other.GetComponent<BotsControl>().SpeedMin(slow_Bots, slowTime);
            
        }
        else if (other.GetComponent<PhotonView>() != null && other.GetComponent<PlayerController>() != null)
        {
            if (other.GetComponent<PhotonView>().ViewID != ID)
            {
                other.GetComponent<PlayerController>().SpeedMin(slow, slowTime);
            }

          
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
