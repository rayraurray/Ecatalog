using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButtonScript : MonoBehaviour
{
    private GameObject _model;
    private RaycastHit _raycastHit;
    private Ray _ray;
    private ResetModel _reset;

    public void OnPress()
    {
        RayCast();
        if (_model == null)
        {
            return;
        }

        _reset = _model.GetComponent<ResetModel>();
        _reset.ResetTransform();
    }

    private void RayCast()
    {
        _ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(_ray, out _raycastHit))
        {
            if (_raycastHit.transform.gameObject.tag == "Models")
            {
                _model = _raycastHit.transform.gameObject;
            }
            else
            {
                _model = GameObject.FindGameObjectWithTag("Models").transform.gameObject;
            }
        }
    }
}
