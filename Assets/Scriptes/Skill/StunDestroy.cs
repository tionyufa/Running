using System;
using UnityEngine;

    public class StunDestroy : MonoBehaviour
    {
        [SerializeField] private float time;
        private void Start()
        {
            Destroy(this.gameObject,time);
        }
    }

