using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class TriggerFire : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject ShockFire;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("wat" + other.name);
            var pos = other.transform.position - new Vector3(0, 0, 0);
            var stun = Instantiate(ShockFire, pos, Quaternion.identity);
            Destroy(stun, 0.4f);
            Destroy(gameObject);
        }

    }
    
}
