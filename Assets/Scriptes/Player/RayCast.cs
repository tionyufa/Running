using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayCast : MonoBehaviour 
{
    private Ray ray;
    [SerializeField] private float direction;
    [SerializeField] private PlayerController _playerController;

    private void Update()
    {
        Raycast_Player();
    }



    private void Raycast_Player()
    {
        if (Input.touchCount > 0)
        {
            Touch [] myTouch = Input.touches;
            for (int i = 0; i < myTouch.Length; i++)
            {
                Ray ray = Camera.main.ScreenPointToRay(myTouch[i].position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                  
                }
            }           
        }
    }


   
}
