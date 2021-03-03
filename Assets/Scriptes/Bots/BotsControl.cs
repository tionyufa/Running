using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class BotsControl : MonoBehaviourPunCallbacks
{
   [SerializeField] private NavMeshAgent _agent;
   [SerializeField] private List <Transform> check;
   [SerializeField] private Animator _animator;
   [SerializeField] private Rigidbody rb;
   [SerializeField] private Vector3 height; 
   [SerializeField] private float TimeRun;
   [SerializeField] private bool _start;
   
   public PhotonView _photonView;
   public bool _finish;
   public bool isShield = false;
   private PointPosition _pointPosition;
   
     [Header("Skill")]
     [SerializeField] private float force;
     private int skillID;
     private int IDpoint = 0;
     private float timeSlow;
     private float speedMinus;
     private Ray ray;
     bool isStun;
     Transform positEnemy;
     private Vector3 vector3Enemy;

     private void Awake()
     {
         
         _pointPosition = PointPosition.point;
         check = _pointPosition.position;
         vector3Enemy = transform.position + Vector3.forward;

     }

     

     private void Update()
     { 
         
         timeSlow -= Time.deltaTime;
       TimeRun -= Time.deltaTime;
       if (TimeRun < 0)
       {
           TimeRun = 0;
       }

       if (_start)
       {
           Navmesh();
       }

      
   }

   public void start_bots(bool isStart)
   {
       _start = isStart;
   }
   
   public void Stun_Time(float value)
   {
       TimeRun = value;
   }

   public void Navmesh()
   {
       if (TimeRun <= 0 )
       {
           if (timeSlow > 0)
           {
               _agent.speed = speedMinus;
           }
           else if (timeSlow <= 0.0001f)
           {
               _agent.speed = 10f;
           }
           _agent.SetDestination(check[IDpoint].position);
           _animator.SetFloat("Run", 0.1f);
           _animator.SetBool("Fire", false);
           _animator.SetBool("Shock", false);
           _animator.SetBool("Slow", false);
           _animator.SetBool("Stun", false);
           isStun = false;
       }
       else if (TimeRun > 0)
       {
           _agent.speed = 0;
           _animator.SetBool("Stun", true);           
       }
   }

   private void OnTriggerEnter(Collider other)
   {
       if (other.CompareTag("Point"))
       {
           
           if (IDpoint < check.Count)
           {
               IDpoint++;
           }
       }

       if (other.CompareTag("Bonus"))
       {
          
           Skill();
       }
   }

   public void SpeedMin(float value, float _slowTime)
   {
       timeSlow = _slowTime;
       speedMinus = _agent.speed - value;
       if (speedMinus < 8f)
       {
           speedMinus = 8f;
       }
   }
   public void Skill()
   {
       
           skillID = Random.Range(0, 4);
           float time = Random.Range(0, 4);
           switch (skillID)
           {
               case 0:
                   Invoke("Force", time);
                   break;
               case 1:
                   Invoke("Fire", time);
                   break;
               case 2:
                   Invoke("Shock", time);
                   break;
               case 3:
                   Invoke("Shield", time);
                   break;
               case 4:
                   Invoke("Slow", time);
                   break;
           }
       
       
       
   }

   private void Slow()
   {
       _animator.SetBool("Slow",true);
      
   }

   public void StartSlow_Bots()
   {
       var _clone = PhotonNetwork.Instantiate("PreFab/Partical/Slow",transform.position,Quaternion.identity);
       var _transform = transform;
       _clone.GetComponent<SlowSkill>().setVector(_transform,_transform.rotation);
       _clone.GetComponent<SlowSkill>().ID = _photonView.ViewID;
   }
   private void Shield()
   {
       if (_photonView.IsMine)
       {
           isShield = true;
           var shiled = PhotonNetwork.Instantiate("PreFab/Partical/Shield", transform.position, Quaternion.identity);
           shiled.GetComponent<Shield>().tranf(gameObject.transform);
           Invoke("shieldFalse",5f);
           
       }
   }

   private void shieldFalse()
   {
       isShield = false;
   }

   private void Shock()
   {
       
            _animator.SetBool("Shock", true);
       
   }
   
   
   public void StartShock_Bots()
   {
       var shock = PhotonNetwork.Instantiate("PreFab/Partical/Shock", transform.position, Quaternion.identity);
       shock.GetComponent<StunSkill>().setID(_photonView.ViewID);
   }
   
    private void Fire()
   {
      
           var list = PhotonGameManager.GameManager.listPlayer;
           float max = 0;
           for (int i = 0; i < list.Count; i++)
           {
               float s = gameObject.transform.position.magnitude - list[i].transform.position.magnitude;

               if (s > max)
               {
                   max = s;

                   positEnemy = list[i].transform;
               }

           }

           _animator.SetBool("Fire", true);
     
    }

   [PunRPC]
   public void StartFire_Bots()
   {
       if (positEnemy != null)
       {
           vector3Enemy = positEnemy.transform.position - gameObject.transform.position;
           vector3Enemy.Normalize();
       }

       ray = new Ray(transform.position,vector3Enemy);
       
       Vector3 v = transform.position + height;
       var cloneFire = PhotonNetwork.Instantiate("PreFab/Partical/Fire",v, Quaternion.identity);
       cloneFire.GetComponent<TranslateFire>().setID(ray);
       cloneFire.GetComponent<TranslateFire>().ID = _photonView.ViewID;

   }
   
   
   
   public void Force()
   {
       
       rb.AddForce(Vector3.forward * force,ForceMode.Force);
       
       
   }
   
   public void Stun_Bots()
   {
       if (_photonView.IsMine && !isStun) 
       {
           PhotonNetwork.Instantiate("PreFab/Partical/Star", transform.position + new Vector3(0,2,0), Quaternion.identity);
           isStun = true;
       }
   }
   
   
 }
