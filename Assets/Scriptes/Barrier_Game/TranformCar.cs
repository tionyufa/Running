using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranformCar : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float onTime;
    [SerializeField] private Car_Paritical _carParitical;
    [SerializeField] private Vector3 _vector3;
    [SerializeField] private bool isTranf;
    void FixedUpdate()
    {
        gameObject.transform.Translate(Vector3.forward * speed ,Space.Self); 
        
    }

    private void Update()
    {
        onTime -= Time.deltaTime;
        if (onTime <= 0)
        {
            _carParitical.parcticalON();
        }
    }

    private void OnTriggerStay(Collider other)
    {
       
        if (other.GetComponent<PlayerController>() && !other.GetComponent<PlayerController>().isShield && isTranf)
        {
            other.transform.position = gameObject.transform.position + _vector3;
        }
       
        //Bot
        else if  (other.GetComponent<BotsControl>() && !other.GetComponent<BotsControl>().isShield && isTranf)
        {
            other.transform.position = gameObject.transform.position + _vector3;
        }
       
        
    }
}
