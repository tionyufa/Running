using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;



public class PanelText : MonoBehaviour
{
   [SerializeField] private GameObject panel;
   [SerializeField] private GameObject panelError;
   [SerializeField] private Text texterror;
   [SerializeField] private Text _text;
   [SerializeField] private Button Yes;
   [SerializeField] private Button Esc;
   public static PanelText instation { get; private set; }


   private void Start()
   {
      instation = this;
   }

   public void DialogShow (string text,UnityAction _yesAction,UnityAction _escAction)
   {
      _text.text = text;
      panel.SetActive(true);
      
      Yes.onClick.RemoveAllListeners();
      Yes.onClick.AddListener(_yesAction);
      Yes.onClick.AddListener(ClosePanel);
      
      Esc.onClick.RemoveAllListeners();
      Esc.onClick.AddListener(_escAction);
      Esc.onClick.AddListener(ClosePanel);      
   }

   public void DialogError(string text)
   {
      panel.SetActive(false);
      panelError.SetActive(true);
      texterror.text = text;
   }

   public void ClosePanel()
   {
      panel.SetActive(false);
   }
}
