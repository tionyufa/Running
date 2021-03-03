using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class SkillPlayer : MonoBehaviourPunCallbacks 
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject arrowImage;
    [SerializeField] private float timercanvas;
    private float timeRun;
    private bool isTuchJoy;
    private Ray ray;

    private void Update()
    {
        timercanvas -= Time.deltaTime;
        if (timercanvas < 0)
        {
            timercanvas = 0;
        }

        if (timercanvas > 0.0001)
        {
            rotationCanvas();
        }
    }

    public void RunTime(float isTime)
    {
        timeRun = isTime;
    }

    public void isTuch(bool tuch)
    {
        isTuchJoy = tuch;
    }
    public void rotationCanvas()
    {
        
        RaycastHit hit;
        Touch [] mytouch = Input.touches;
        for (int i = 0; i < mytouch.Length; i++)
        {
            if (mytouch[i].phase == TouchPhase.Began)
            {
               
            }
            else if (mytouch[i].phase == TouchPhase.Moved)
            {
                if (isTuchJoy && mytouch.Length > 1 )
                {
                    timercanvas = 1f;
                    ray = Camera.main.ScreenPointToRay(mytouch[1].position);
                }
                else if (isTuchJoy == false)
                {
                    ray = Camera.main.ScreenPointToRay(mytouch[i].position);
                }
            }
            else if (mytouch[i].phase == TouchPhase.Canceled)
            {
                arrowImage.SetActive(false);
                timercanvas = 0;
                ray = Camera.main.ScreenPointToRay(mytouch[i].position);
            }
            else if (mytouch[i].phase == TouchPhase.Ended)
            {
                arrowImage.SetActive(false);
                timercanvas = 0;
                ray = Camera.main.ScreenPointToRay(mytouch[i].position);
            }
        }


        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Quaternion tranRot = Quaternion.LookRotation(hit.point - transform.position);
            tranRot.z = 0;
            tranRot.Normalize();
            _canvas.transform.rotation = Quaternion.Lerp(tranRot,_canvas.transform.rotation,0f);
             
            
        }
    }

    public void IceTuch()
    {
        if (_playerController._photonView.IsMine && timeRun <= 0)
        {
            timercanvas = 5f;
            _playerController.AnimatorIce(_canvas.transform.rotation);
            arrowImage.SetActive(false);
        }
    }

    public void StartIce()
    {
        if (_playerController._photonView.IsMine && timeRun <= 0)
        {
            arrowImage.SetActive(true);
            timercanvas = 100f;
        }
    }

    public void FireTuch()
    {
        if (_playerController._photonView.IsMine && timeRun <= 0)
        {
            timercanvas = 5f;
            _playerController.AnimatorFire(ray);
            arrowImage.SetActive(false);
        }
    }

    public void StartFire()
    {
        if (_playerController._photonView.IsMine && timeRun <= 0)
        {
            arrowImage.SetActive(true);
            timercanvas = 100f;
        }

    }
     
}
