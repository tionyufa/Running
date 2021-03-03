using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class nameNick : MonoBehaviourPunCallbacks , IPunObservable
{
    [SerializeField] private TextMeshProUGUI nicknameText;
    [SerializeField] private PhotonView _photonView;
    private string _name = "";

    private void Awake()
    {
               
        if (_photonView.IsMine) {
            _name = PhotonNetwork.NickName;
            nicknameText.text = _name;
            
        }
        else
        {
            return;
        }

    }
    

    private void Update()
    { 
        if (_photonView.IsMine) 
        nicknameText.text = _name;
    }
    
    public void setName(string _name , Color color)
    {
        this._name = _name;
        nicknameText.color = color;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(nicknameText.text);
        }
        else
        {
            nicknameText.text = (string) stream.ReceiveNext();
        }
    }
}
