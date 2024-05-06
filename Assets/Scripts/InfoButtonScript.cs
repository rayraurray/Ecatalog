using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _panels;
    [SerializeField] private bool _enabled;

    private void Awake()
    {
        _enabled = true;
        _panels = GameObject.FindGameObjectsWithTag("Info Panel");
    }

    public void OnButtonClicked()
    {
        _enabled = !_enabled;
        EnableInfo();
    }

    private void EnableInfo()
    {
        if (_enabled)
        {
            foreach (var panel in _panels)
            {
                panel.SetActive(true);
            }
        }
        else
        {
            foreach (var panel in _panels)
            {
                panel.SetActive(false);
            }
        }
    }
}
