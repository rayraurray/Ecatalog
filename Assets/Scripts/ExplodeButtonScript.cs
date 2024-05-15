using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeButtonScript : MonoBehaviour
{
    private bool _exploded;
    private Animator _animator;

    public void OnButtonClicked()
    {
        Explode();
    }

    private void Explode()
    {
        _animator = GameObject.FindGameObjectWithTag("Models").GetComponent<Animator>();

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
