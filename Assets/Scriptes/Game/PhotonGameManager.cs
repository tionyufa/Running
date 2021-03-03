using System.Collections.Generic;
using Cinemachine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PhotonGameManager : MonoBehaviourPunCallbacks 
{
    public static PhotonGameManager GameManager { get; private set; }
   [SerializeField] private Text textmessage;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private GameObject _startGame;
    private PhotonView _photonView;
    private Vector3 _spawn; 
    
    [Header("Spawn")]
    [SerializeField] private SpawnPlayer _spawnPlayer;
    public List <GameObject> listPlayer;
    private Player[] allplayer;
    
    int ID;
    private int ID_bots;
    
    public void Start()
    {
        GameManager = this;
        allplayer = PhotonNetwork.PlayerList;
        foreach (var p in allplayer)
        {
            if (p != PhotonNetwork.LocalPlayer)
            {
                ID++;
            }
            
        }
        
        _spawn = _spawnPlayer.spawnPosition(ID).position;
        var player = PhotonNetwork.Instantiate(PlayerPrefs.GetString("Path"), _spawn, Quaternion.identity);
        listPlayer.Add(player);
        player.name = "Player_" + PhotonNetwork.NickName;
        _camera.Follow = player.transform;
        _camera.LookAt = player.transform;
        
        if (PhotonNetwork.IsMasterClient)
        {
        _startGame.SetActive(true);
        }
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("4011935",false);
        }
   
    }

   
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public void Leave()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("video");
        }
        PhotonNetwork.LeaveRoom();
    }

    public void CreateBots(int how,bool isCreate)
    {
        if (isCreate)
        {
            for (int i = 0; i < how; i++)
            {
                 
                _spawn = _spawnPlayer.spawnPositionBots(ID_bots).position;
                ID_bots++;
                var bots = PhotonNetwork.Instantiate("Bot", _spawn, Quaternion.identity);
                float s = i ;
                bots.GetComponent<nameNick>().setName("Bot " + s, Color.red);
                listPlayer.Add(bots);
            }
        }
    }
    

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Log(newPlayer.NickName + " Join to Room");
        
        
    }
    
    

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Log(otherPlayer.NickName + " Left to Room");
    }

    public void Log(string message)
    {
        textmessage.text = message;
        Debug.Log(message);
        
    }
    
   
}
