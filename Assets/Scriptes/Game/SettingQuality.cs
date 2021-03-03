using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingQuality : MonoBehaviour
{
   [SerializeField]  Dropdown dropdown;
   private List<string> names;

   
   public void checkdropdown()
    {
        
        QualitySettings.SetQualityLevel(dropdown.value, true);
    }
}
