using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Button_Room : MonoBehaviour
{
   [SerializeField] private Text Name;
   [SerializeField] private Text Size;
   [SerializeField] private string namestring;
   private PanelText dialog;
   private void Start()
   {
      dialog = PanelText.instation;
   }

   public void RoomValue(string name,string size , string maxPlayer)
   {
      namestring = name;
      Name.text = String.Format("Name Room - <color=#66E275> {0} </color>",name);
      Size.text = String.Format("<color=#3FBF3F> {0} </color> / {1}",size,maxPlayer);
   }

   public void onCLICKJoinRoom()
   {
      string str = String.Format("Do you want to join a room \n Name Room - <color=#3FBF3F> {0} </color>",namestring);
      dialog.DialogShow(str,JointRoom,dialog.ClosePanel);
      
   }

   private void JointRoom()
   {
      if (PlayerPrefs.GetString("Player").Length >= 3)
      {
         PhotonNetwork.JoinRoom(namestring);
      }
      else if (PlayerPrefs.GetString("Player").Length < 2)
      {
         dialog.DialogError("Nickname must be more than 3 characters");
      }
   }
}
