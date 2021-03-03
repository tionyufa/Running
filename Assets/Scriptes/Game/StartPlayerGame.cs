using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StartPlayerGame : MonoBehaviour , IPunObservable
{
    private bool start;
    
    [SerializeField] private StartGame startGame;
    
    [SerializeField] private Toggle _toggle;

    private void Awake()
    {
        
    }

    public void StartGame(bool start_)
    {
        if (PhotonGameManager.GameManager.listPlayer.Count >= 1) // количество
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;

            int countbots = PhotonNetwork.CurrentRoom.MaxPlayers - PhotonNetwork.CurrentRoom.PlayerCount;
            StartCoroutine(CreateBots(countbots));
            start = start_;
           
            startGame.isGame(start);
           
            
            Invoke("startRunBots", 5f);
        }
    }

    void startRunBots()
    {
        for (int i = 0; i < PhotonGameManager.GameManager.listPlayer.Count; i++)
        {
            if (PhotonGameManager.GameManager.listPlayer[i].GetComponent<BotsControl>() != null)
                PhotonGameManager.GameManager.listPlayer[i].GetComponent<BotsControl>().start_bots(true);
           
        }
    }

    IEnumerator CreateBots(int i)
    {
        PhotonGameManager.GameManager.CreateBots(i,_toggle.isOn);
        yield return new WaitForSeconds(1f);
    }
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(start);
            

        }
        else if (stream.IsReading)
        {
            start = (bool) stream.ReceiveNext();
        }
    }
}
