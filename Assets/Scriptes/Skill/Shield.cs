using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private Vector3 _vector3;
    private Transform _transform;
    public void tranf(Transform transform)
    {
        _transform = transform;
    }
    void Update()
    {
        if (_transform  != null)
        transform.position = _transform.position;
    }
}
