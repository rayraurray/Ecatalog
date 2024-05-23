using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoManager : MonoBehaviour
{
    public static InfoManager _instance;
    public TextMeshProUGUI _modelName;
    public TextMeshProUGUI _modelDescription;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void ShowInfo(string name, string description)
    {
        _modelName.text = name;
        _modelDescription.text = description;
    }

    public void HideInfo()
    {
        _modelName.text = string.Empty;
        _modelDescription.text = string.Empty;
    }
}
