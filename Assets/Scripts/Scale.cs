using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scale : MonoBehaviour
{
    #region Scale Parameters
    [SerializeField] private float _factor = 0.05f;
    private Vector3 originalScale;
    private Vector3 originalPosition;
    #endregion
    #region InputSystem Parameters
    [SerializeField] private TouchAction _actions;

    public TouchAction Actions
    {
        get => _actions;
        set => _actions = value;
    }
    protected InputAction PrimaryFingerPos { get; set; }
    protected InputAction SecondaryFingerPos { get; set; }
    protected InputAction SecondaryTouchContact { get; set; }
    protected InputAction MouseScrollUp { get; set; }
    protected InputAction MouseScrollDown { get; set; }
    #endregion
    private Coroutine _scaleCoroutine;
    private Transform _modelTransform;
    private RaycastHit _raycastHit;
    private Ray _ray;

    private void OnEnable()
    {
        InitializeInputSystem();
        Actions.Enable();
    }

    private void OnDisable()
    {
        Actions.Disable();
    }

    private void Awake()
    {
        Actions = new TouchAction();
    }

    private void Start()
    {
        SecondaryTouchContact.started += _ => ScaleStart();
        SecondaryTouchContact.canceled += _ => ScaleEnd();

        MouseScrollUp.performed += ctx => ScrollScaleUp();
        MouseScrollDown.performed += ctx => ScrollScaleDown();
    }

    protected void InitializeInputSystem()
    {
        PrimaryFingerPos = Actions.FindAction("PrimaryFingerPos");
        SecondaryFingerPos = Actions.FindAction("SecondaryFingerPos");
        SecondaryTouchContact = Actions.FindAction("SecondaryTouchContact");
        MouseScrollUp = Actions.FindAction("MouseScrollUp");
        MouseScrollDown = Actions.FindAction("MouseScrollDown");
    }

    private void ScaleStart()
    {
        RayCast();
        _scaleCoroutine = StartCoroutine(ScaleDetection());
    }

    private void RayCast()
    {
        _ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(_ray, out _raycastHit))
        {
            if (_raycastHit.transform.gameObject.tag == "Models")
                _modelTransform = _raycastHit.transform;
            else
                _modelTransform = GameObject.FindGameObjectWithTag("Models").transform;
        }
    }

    private void ScaleEnd()
    {
        StopCoroutine(_scaleCoroutine);
    }

    IEnumerator ScaleDetection()
    {
        float previousDistance = 0f;
        float currentDistance = 0f;

        while (true)
        {
            currentDistance = Vector2.Distance(PrimaryFingerPos.ReadValue<Vector2>(),
                                               SecondaryFingerPos.ReadValue<Vector2>());

            if (currentDistance > previousDistance)
            {
                ScaleModel(true);
            }
            else if (currentDistance < previousDistance)
            {
                ScaleModel(false);
            }

            previousDistance = currentDistance;
            yield return null;
        }
    }

    private void ScaleModel(bool up)
    {
        if (_modelTransform == null)
        {
            return;
        } 
        else
        {
            float factor = up ? _factor : -(_factor);

            _modelTransform.localScale *= 1 + factor;
            _modelTransform.localPosition = new Vector3(_modelTransform.localPosition.x,
                                                        _modelTransform.localPosition.y * (1 + factor),
                                                        _modelTransform.localPosition.z);
        }
    }

    private void ScrollScaleUp()
    {
        RayCast();
        ScaleModel(true);
    }

    private void ScrollScaleDown()
    {
        RayCast();
        ScaleModel(false);
    }
}
