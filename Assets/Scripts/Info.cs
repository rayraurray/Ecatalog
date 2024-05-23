using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour
{
    [SerializeField] string _modelName;
    [SerializeField] string _modelDescription;

    public void SendInfo()
    {
        InfoManager._instance.ShowInfo(_modelName, _modelDescription);  
    }

    public void Hide()
    {
        InfoManager._instance.HideInfo();
    }
}
