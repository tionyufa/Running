using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class AddSkill : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            if (other.gameObject.GetComponent<PlayerController>()._photonView.IsMine)
            {

                other.GetComponent<PlayerController>().ActiveSkill();
                Destroy(this.gameObject);
            }
        }
       
        
    }

   
    
    
}
