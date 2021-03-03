using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPosition : MonoBehaviour
{
    public static PointPosition point { get; private set; } 
    public List<Transform> position;

    private void Start()
    {
        point = this;
    }
}
