using UnityEngine;

public class ScaleButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _models;
    [SerializeField] private bool _enabled = false;

    public void OnButtonClicked()
    {
        _enabled = !_enabled;
        EnableScale();
    }

    private void EnableScale()
    {
        _models = GameObject.FindGameObjectsWithTag("Models");

        if (_enabled)
        {
            foreach (var model in _models)
            {
                model.GetComponent<ScaleModels>().enabled = true;
            }
        }
        else
        {
            foreach (var model in _models)
            {
                model.GetComponent<ScaleModels>().enabled = false;
            }
        }
    }
}