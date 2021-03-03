using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoisePlayer : MonoBehaviour
{
   [SerializeField] private GameObject player, player1;

   private void Awake()
   {
      if (!PlayerPrefs.HasKey("Path"))
      {
         PlayerPrefs.SetString("Path","Player");
      }
   }

   private void Start()
   {
      
      if (PlayerPrefs.GetString("Path") == "Player")
      {
         player.SetActive(true);
         player1.SetActive(false);
      }
      else if (PlayerPrefs.GetString("Path") == "Player_1")
      {
         player.SetActive(false);
         player1.SetActive(true);
      }
   }

   public void setName(string path)
   {
      PlayerPrefs.SetString("Path",path);
      
      if (PlayerPrefs.GetString("Path") == "Player")
      {
         player.SetActive(true);
         player1.SetActive(false);
      }
      else if (PlayerPrefs.GetString("Path") == "Player_1")
      {
         player.SetActive(false);
         player1.SetActive(true);
      }
   }
}
