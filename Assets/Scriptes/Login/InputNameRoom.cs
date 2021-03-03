using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputNameRoom : MonoBehaviour
{
   [SerializeField] private InputField _inputField;
   [SerializeField] private GameObject Button;

   private void Start()
   {
     
   }

   public void SetName()
   {
      if (_inputField.text.Length < 1)
      {
         Button.SetActive(false);
      }
      else if (_inputField.text.Length >= 1)
      {
         Button.SetActive(true);
      }
   }
}
