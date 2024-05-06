using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private Transform _object;

    private void Awake()
    {
        _camera = Camera.main;
        _object = transform;
    }

    private void Update()
    {
        _object.rotation = Quaternion.Slerp(_object.rotation, _camera.transform.rotation, _speed * Time.deltaTime);
    }
}
