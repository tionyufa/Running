using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoundTuch : MonoBehaviour , IPointerClickHandler
{
   [SerializeField] private AudioSource _audioSource;


   public void OnPointerClick(PointerEventData eventData)
   {
      _audioSource.Play();
   }
}
