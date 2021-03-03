using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSetting : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject DialogPanel;
    [SerializeField] private PanelText _panelText;
    private bool isTrue;

    public void onSetting()
    {
        if (isTrue)
        {
            panel.SetActive(false);
            isTrue = false;
        }
        else if (isTrue == false)

        {
            panel.SetActive(true);
            isTrue = true;
        }
    }

    public void DialogEsp()
    {
        DialogPanel.SetActive(true);
        panel.SetActive(false);
        string str = String.Format("Are you sure you want to go to the Main Menu?");
        _panelText.DialogShow(str,PhotonGameManager.GameManager.Leave,DialogFalse);
        
    }

    public void DialogFalse()
    {
        DialogPanel.SetActive(false);
    }
    
}
