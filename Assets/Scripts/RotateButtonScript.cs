
using UnityEngine;

public class RotateButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _models;
    [SerializeField] private bool _enabled = false;

    public void OnButtonClicked()
    {
        _enabled = !_enabled;
        EnableRotation();
    }

    private void EnableRotation()
    {
        _models = GameObject.FindGameObjectsWithTag("Models");

        if (_enabled) 
        {
            foreach (var model in _models)
            {
                model.GetComponent<RotateModels>().enabled = true;
            }
        }
        else
        {
            foreach (var model in _models)
            {
                model.GetComponent<RotateModels>().enabled = false;
            }
        }
    }
}
