using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rotate : MonoBehaviour
{
    #region Transform Parameters
    [SerializeField] private float _speed = 12.0f;
    [SerializeField] private bool _inverted = false;
    #endregion
    #region InputSystem Parameters
    [SerializeField] private TouchAction _actions;
    public TouchAction Actions
    {
        get => _actions;
        set => _actions = value;
    }
    protected InputAction LeftClickPressedAction { get; set; }
    protected InputAction MouseLookAction { get; set; }
    #endregion
    private Coroutine _rotateCoroutine;
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
        LeftClickPressedAction.started += _ => RotateStart();
        LeftClickPressedAction.canceled += _ => RotateEnd();
    }

    protected void InitializeInputSystem()
    {
        LeftClickPressedAction = Actions.FindAction("LeftClick");
        MouseLookAction = Actions.FindAction("MouseLook");
    }

    private void RotateStart()
    {
        RayCast();
        _rotateCoroutine = StartCoroutine(RotationDetection());
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

    private void RotateEnd()
    {
        StopCoroutine(_rotateCoroutine);
    }

    IEnumerator RotationDetection()
    {
        while (true)
        {
            Vector2 MouseDelta = GetMouseLookInput();
            MouseDelta *= _speed * Time.deltaTime;
            _modelTransform.Rotate(Vector3.up * (_inverted ? 1 : -1), MouseDelta.x, Space.Self);

            yield return null;
        }
    }

    protected virtual Vector2 GetMouseLookInput()
    {
        if (MouseLookAction != null)
            return MouseLookAction.ReadValue<Vector2>();

        return Vector2.zero;
    }
}