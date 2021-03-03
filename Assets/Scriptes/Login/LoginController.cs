using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;


public class LoginController : MonoBehaviourPunCallbacks
{
   [SerializeField] private string VersionName = "1";
   
   
   [SerializeField] private InputField CreateRoomTextName;
   [SerializeField] private InputField NickNameField;
   [SerializeField] private Transform roompanel;
   [SerializeField] private GameObject roomUIPrefub;
   [SerializeField] private byte countPlayer;

   [Header("Dialog")] 
   [SerializeField] private GameObject PanelTextError;
   [SerializeField] private PanelText _panelText_Scr;
   [SerializeField] private GameObject loading;
   
   
   private void Awake()
   {
     
      if (!PlayerPrefs.HasKey("Player"))
      PlayerPrefs.SetString("Player","Player");
      
     
      PhotonNetwork.OfflineMode = true;
      Screen.sleepTimeout = SleepTimeout.NeverSleep;
      PhotonNetwork.AutomaticallySyncScene = true;
      PhotonNetwork.GameVersion = VersionName;
      PhotonNetwork.ConnectUsingSettings();
      
   }

   private void Start()
   {
      if (PlayerPrefs.HasKey("Player"))
      {
         NickNameField.text = PlayerPrefs.GetString("Player");
      }
      
      Debug.Log(PlayerPrefs.GetString("Player"));
   }

   public void SelectPlayer(int how)
   {
      countPlayer = (byte) how;
      Dialog_Room("CreateGame");
   }

   public override void OnDisconnected(DisconnectCause cause)
   {
      Debug.Log("Dissconect");
   }


   public override void OnConnectedToMaster()
   {
      Log("Connected!");
      

      PhotonNetwork.JoinLobby();
   }


   public void Dialog_Room(string name)
   {
      if (name == "CreateGame")
      {
         string str = string.Format("Are you sure you want to create a room? \n Room Name - " + "<color=#3FBF3F> {0} </color>" + "\n  Max Player - <color=#0969A2> {1} </color>"  , CreateRoomTextName.text,countPlayer );
         _panelText_Scr.DialogShow(str, CreateRoom,_panelText_Scr.ClosePanel);
      }
      else if (name == "JoinRandom")
      {
         string str = string.Format("Do you want to join a Random room?");
         _panelText_Scr.DialogShow(str, JoinRandomRoom,_panelText_Scr.ClosePanel);
      }
      else if (name == "ExitGame")
      {
         string str = string.Format("Are you sure you want to quit the game?");
         _panelText_Scr.DialogShow(str, Leave_game,_panelText_Scr.ClosePanel);
      }
      
   }
   
   public  void CreateRoom()
   {
      if (NickNameField.text.Length >= 3)
      {
         RoomOptions roomOptions = new RoomOptions() {IsOpen = true, IsVisible = true, MaxPlayers = countPlayer};
         PanelTextError.SetActive(true);
         PhotonNetwork.CreateRoom(CreateRoomTextName.text, roomOptions);
         loading.SetActive(true);
      }
      else if (NickNameField.text.Length < 2)
      {
         string _namestr = "Nickname must be more than 3 characters";
         _panelText_Scr.DialogError(_namestr);
      }
   }

   public override void OnCreateRoomFailed(short returnCode, string message)
   {
      OnConnectedToMaster();
   }
   
   

   public override void OnRoomListUpdate(List<RoomInfo> roomList)
   {
      foreach (var room in roomList)
      {
         if (room.PlayerCount == 0)
         {
            room.IsVisible = false;
         }
      }
      for (int i = 0; i < roomList.Count; i++)
      {
         ListRoom(roomList[i]);
      }
      base.OnRoomListUpdate(roomList);
   }

  
   public void RemoveRoom()
   {
      while (roompanel.childCount != 0)
      {
       Destroy(roompanel.GetChild(0));  
      }
   }

   public void ListRoom(RoomInfo room)
   {
      if (room.IsOpen && room.IsVisible)
      {
         GameObject tempListing = Instantiate(roomUIPrefub.gameObject, roompanel);
         Button_Room roomButton = tempListing.GetComponent<Button_Room>();
         roomButton.RoomValue(room.Name, room.PlayerCount.ToString(), room.MaxPlayers.ToString());
         
      }

   }
   

   public void JoinRandomRoom()
   {
      if (NickNameField.text.Length >= 3)
      {
         
         PhotonNetwork.JoinRandomRoom();
      }
      else if (NickNameField.text.Length < 2)
      {
         string _namestr = "Nickname must be more than 3 characters";
         _panelText_Scr.DialogError(_namestr);
      }
     
   }

   public override void OnJoinRandomFailed(short returnCode, string message)
   {
      _panelText_Scr.DialogError("Sorry, we didn't find a room, try to create it yourself");
   }

   public void JoinRoom(string roomName)
   {
      PhotonNetwork.JoinRoom(roomName);
   }
   
   public override void OnJoinedRoom()
   {
      if (PhotonNetwork.CurrentRoom.PlayerCount != 8)
      {
         PhotonNetwork.LoadLevel(1);
      }
   }

   public override void OnJoinedLobby()
   {
      Debug.Log("Join Lobby");
     
   }

   public override void OnCreatedRoom()
   {
      base.OnCreatedRoom();
     
   }

   public void Log(string message)
   {
      
      Debug.Log(message);
   }

   public void NickName()
   {
      PlayerPrefs.SetString("Player",NickNameField.text);
      PhotonNetwork.NickName = NickNameField.text;
   }

   public override void OnPlayerEnteredRoom(Player newPlayer)
   {
      if (PhotonNetwork.CurrentRoom.PlayerCount >= countPlayer)
      {
         PhotonNetwork.CurrentRoom.IsOpen = false;
         
      }
      else
      {
         PhotonNetwork.CurrentRoom.IsOpen = true;
      }
   }

   private void OnConnectedToServer()
   {
      Log("Connect to Server");
   }

   public void Leave_game()
   {
      Application.Quit();
   }
}


