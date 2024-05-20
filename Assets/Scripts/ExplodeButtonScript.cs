using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplodeButtonScript : MonoBehaviour
{
    [SerializeField] private Button _btn;
    private float _wait = 2f;
    private Animator _animator;
    private Coroutine _buttonDisabled;
    private GameObject _model;
    private RaycastHit _raycastHit;
    private Ray _ray;

    public void OnPress()
    {
        RayCast();
        Explode();
        _buttonDisabled = StartCoroutine(DisableButtonALil(_wait));
    }

    private IEnumerator DisableButtonALil(float wait)
    {
        _btn.interactable = false;

        yield return new WaitForSeconds(wait);

        _btn.interactable = true;
    }

    private void RayCast()
    {
        _ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(_ray, out _raycastHit))
        {
            if (_raycastHit.transform.gameObject.tag == "Models")
                _model = _raycastHit.transform.gameObject;
            else
                _model = GameObject.FindGameObjectWithTag("Models").transform.gameObject;
        }
    }

    private void Explode()
    {
        if (_model == null)
            return;
        else
        {
            _animator = _model.GetComponent<Animator>();

            if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Explode"))
            {
                _animator.SetTrigger("Explode");
            }
            else
            {
                _animator.SetTrigger("Unexplode");
            }
        }
    }
}
