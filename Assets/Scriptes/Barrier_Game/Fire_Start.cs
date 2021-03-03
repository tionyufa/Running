using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Start : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private bool isTrue;
    [SerializeField] private Animator _animator;
    private float time;
    
    private void Update()
    {
        time += Time.deltaTime;
        if (time > 3)
        {
            time = -3f;
        }

        if (_particleSystem.isStopped && time > 0)
        {
            _particleSystem.Play();
            _animator.SetBool("IsFire",true);
        }

        else if (_particleSystem.isPlaying && time < 0)
        {
            _particleSystem.Stop();
            _animator.SetBool("IsFire",false);
        }

    }
      
}
