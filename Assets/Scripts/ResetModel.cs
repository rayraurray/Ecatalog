using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetModel : MonoBehaviour
{
    [SerializeField] private GameObject _model;
    private Vector3 _defaultPosition;
    private Quaternion _defaultRotation;
    private Vector3 _defaultScale;

    private void Awake()
    {
        _defaultPosition = _model.transform.localPosition;
        _defaultRotation = _model.transform.localRotation;
        _defaultScale = _model.transform.localScale;
    }

    public void ResetTransform()
    {
        _model.transform.localPosition = _defaultPosition;
        _model.transform.localScale = _defaultScale;
        _model.transform.localRotation = _defaultRotation;
    }
}
