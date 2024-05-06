using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScaleModels : MonoBehaviour
{
    #region Scale Parameters
    [SerializeField] private float _factor = 0.001f;
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

        MouseScrollUp.performed += ctx => Scale(true);
        MouseScrollDown.performed += ctx => Scale(false);
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
        _scaleCoroutine = StartCoroutine(ScaleDetection());
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
            
            if(currentDistance > previousDistance) 
            {
                Scale(true);
            } 
            else if (currentDistance < previousDistance)
            {
                Scale(false);
            }
            
            previousDistance = currentDistance;
            yield return null;
        }
    }

    private void Scale(bool up)
    {
        float factor = up ? _factor : -(_factor);
        
        transform.localScale *= 1 + factor;
        transform.localPosition = new Vector3(transform.localPosition.x, 
                                              transform.localPosition.y * (1 + factor), 
                                              transform.localPosition.z);
    }
}