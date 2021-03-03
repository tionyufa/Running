using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour, IPunObservable
{
    [SerializeField] private GameObject door;
    [SerializeField] private Text _text;
    [SerializeField] private bool _startGame = false;
    [SerializeField] private float time = 5.5f;
    [SerializeField] private AudioSource Startaudio;
    [SerializeField] private AudioSource GameAudio;
    [SerializeField] private Timer _timer;
    [SerializeField] private GameObject panelStarGame;
    [SerializeField] private GameObject AxeFire;
    private bool _audio = true;
    private bool _audiogame = true;
    private void Update()
    {
        if (_startGame)
        Open();
    }

    public void isGame (bool isGame)
    {
        _startGame = isGame;
        
    }
    public void Open()
    {
        if (_audio)
        {
            StartAudio();
        }
        
        if (time > 0)
        {
            time -= Time.deltaTime;
            _text.text = Mathf.Round(time).ToString();
        }
        else if (time < 0)
        {
            _text.text = "START";
            if (_audiogame)
            {
                StartGameAudio();
            }

            Invoke("active",1f);
        } 
        if (time < 1f)
        {
           
            door.transform.Translate(0f, -5f * Time.deltaTime, 0f);
            Destroy(door.gameObject,5f);
        }
    }

    private void active()
    {
        _text.gameObject.SetActive(false);
        _startGame = false;
        
    }

    private void StartGameAudio()
    {
        GameAudio.Play();
        _timer.boolTime(_startGame);
        _audiogame = false;
        panelStarGame.SetActive(false);
        
    }
    private void StartAudio()
    {
        Startaudio.Play();
        _audio = false;
        AxeFire.SetActive(true);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_startGame);
            

        }
        else if (stream.IsReading)
        {
            _startGame = (bool) stream.ReceiveNext();
        }
    
    }
}
