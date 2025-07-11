using System.Collections.Generic;
using UnityEngine;

public class ObjectActivationSwitch : MonoBehaviour
{
    public bool IsPowerOn;
    public List<GameObject> Objects;

    private void OnEnable()
    {
        foreach (var obj in Objects)
            obj.SetActive(IsPowerOn);
    }

    private void OnDisable()
    {
        foreach (var obj in Objects)
            obj.SetActive(!IsPowerOn);
    }
}
