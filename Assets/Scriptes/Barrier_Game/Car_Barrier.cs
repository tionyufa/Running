using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Barrier : MonoBehaviour
{
   [SerializeField] private Transform check;
   [SerializeField] private float speed;
   [SerializeField] private int destrTime;
   [SerializeField] private AudioSource _audioSource;
   private float timer;
   [SerializeField] private Car_Paritical _carParitical;
   [SerializeField] private bool isnotCar;
   [SerializeField] private float parTime = 3f;

   private void Start()
   {
       _carParitical.Invoke("parcticalON",parTime);
       
   }

   private void Update()
   {
       
       StartCar();
       
   }

  

   public void StartCar()
   {
       if (transform.position != check.position)
       {
           transform.position = Vector3.MoveTowards(transform.position, check.position, speed * Time.deltaTime);
           transform.rotation = Quaternion.Euler(0,65,Time.deltaTime * -1);
           timer += Time.deltaTime;
           
       }

       if (isnotCar == true)
       {
           transform.rotation = Quaternion.Euler(speed * Time.deltaTime,0,0);
       }
   }

  

   
}
