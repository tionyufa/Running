using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Finish : MonoBehaviour , IPunObservable
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Text _text_Time;
    [SerializeField] private Text _text_Place;
    [SerializeField] private Timer _timer;
    [SerializeField] private AudioSource finishAudio;
    [SerializeField] private ValueTextSound _valueTextSound;
    public int place = 1;

    private void Start()
    {
        place = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PhotonView>().IsMine && other.GetComponent<PlayerController>() != null)
        {
            other.gameObject.GetComponent<PlayerController>().enabled = false;
            panel.SetActive(true);
            _timer.boolTime(false);
            foreach (var sound in _valueTextSound._audioSource)
            {
                sound.Stop();
            }
            
            finishAudio.Play();
            _text_Place.text = string.Format("Congratulations!!\nYou borrowed - " + place);
            _text_Time.text = string.Format(_timer.text.text);
            place++;
            return;
        }
        else if (other.GetComponent<PhotonView>() && other.GetComponent<BotsControl>() != null && !other.GetComponent<BotsControl>()._finish )
        {
            
            other.GetComponent<BotsControl>().enabled = false;
            other.GetComponent<NavMeshAgent>().enabled = false;
            other.GetComponent<BotsControl>()._finish = true;
            other.gameObject.SetActive(false);
            place++;
            return;
        }
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(place);
        }
        else if (stream.IsReading)
        {
            place = (int) stream.ReceiveNext();
        }
    }
}
