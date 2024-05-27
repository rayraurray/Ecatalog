using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lift : MonoBehaviour
{
    #region Transform Parameters
    [SerializeField] private float _speed = 0.2f;
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
        LeftClickPressedAction.started += _ => LiftStart();
        LeftClickPressedAction.canceled += _ => LiftEnd();
    }

    protected void InitializeInputSystem()
    {
        LeftClickPressedAction = Actions.FindAction("LeftClick");
        MouseLookAction = Actions.FindAction("MouseLook");
    }

    private void LiftStart()
    {
        RayCast();
        if (_modelTransform == null)
            return;

        _rotateCoroutine = StartCoroutine(LiftDetection());
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

    private void LiftEnd()
    {
        StopCoroutine(_rotateCoroutine);
    }

    IEnumerator LiftDetection()
    {
        while (true)
        {
            if (_modelTransform == null)
                yield return null;
            else
            {
                Vector2 MouseDelta = GetMouseLookInput();
                MouseDelta *= _speed * Time.deltaTime;

                Vector3 dir = Vector3.up * (MouseDelta.y * (_inverted ? -1 : 1));

                _modelTransform.Translate(dir, Space.World);

                yield return null;
            }
        }
    }

    protected virtual Vector2 GetMouseLookInput()
    {
        if (MouseLookAction != null)
            return MouseLookAction.ReadValue<Vector2>();

        return Vector2.zero;
    }
}
