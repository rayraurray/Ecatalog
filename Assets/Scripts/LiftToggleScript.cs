using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiftToggleScript : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Animator _animator;
    [SerializeField] GameObject _camera;

    private void Awake()
    {
        _animator = _toggle.GetComponent<Animator>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public void OnToggle()
    {
        _camera.GetComponent<Lift>().enabled = !_camera.GetComponent<Lift>().enabled;
        _animator.SetBool("Activated", !_animator.GetBool("Activated"));
    }
}
