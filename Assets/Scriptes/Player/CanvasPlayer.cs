using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPlayer : MonoBehaviour
{
   [SerializeField] private Canvas _canvas;

   private void Awake()
   {
      _canvas.worldCamera = Camera.main;
   }
}
