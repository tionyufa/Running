using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ValueTextSound : MonoBehaviour
{
    public AudioSource [] _audioSource;
    [SerializeField] private Slider slider;
    [SerializeField] private Text text;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            for (int i = 0; i < _audioSource.Length; i++)
            {
                _audioSource[i].volume = PlayerPrefs.GetFloat("Sound") / 100;
            }
            
            slider.value = PlayerPrefs.GetFloat("Sound") / 100;
        }
    }

    

    private void OnGUI()
    {
        for (int i = 0; i < _audioSource.Length; i++)
        {
            
            float value = Mathf.Round(_audioSource[i].volume * 100);
            PlayerPrefs.SetFloat("Sound",value);
            text.text = PlayerPrefs.GetFloat("Sound").ToString();
        }
        
       
    }
}
