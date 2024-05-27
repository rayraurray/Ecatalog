using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoButtonScript : MonoBehaviour
{
    [SerializeField] private Button _btn;
    [SerializeField] private GameObject _panel;
    [SerializeField] private bool _enabled;
    private Info _info;
    private GameObject _model;
    private RaycastHit _raycastHit;
    private Ray _ray;

    private void Awake()
    {
        _enabled = false;
    }

    public void OnPress()
    {
        _enabled = !_enabled;

        RayCast();

        if (_model == null)
            return;

        if (_enabled)
        {
            ShowInfoPanel();
            Debug.Log("Shown");
        }
        else if (!_enabled)
        {
            HideInfoPanel();
            Debug.Log("Hidden");
        }
    }

    private void RayCast()
    {
        _ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(_ray, out _raycastHit))
        {
            if (_raycastHit.transform.gameObject.tag == "Models")
            {
                _model = _raycastHit.transform.gameObject;
                _info = _model.GetComponent<Info>();
            }
            else
            {
                _model = GameObject.FindGameObjectWithTag("Models").transform.gameObject;
                _info = _model.GetComponent<Info>();
            }
        }
    }

    private void ShowInfoPanel()
    {
        if (_model == null)
            return;

        _info.SendInfo();
        _panel.SetActive(true);
    }

    private void HideInfoPanel()
    {
        if (_model == null)
            return;

        _info.Hide();
        _panel.SetActive(false);    
    }
}
