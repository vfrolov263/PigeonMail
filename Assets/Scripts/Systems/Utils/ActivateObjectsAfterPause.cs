using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ActivateObjectsAfterPause : MonoBehaviour
{
    public int DelayInMilliseconds = 1000;
    public List<GameObject> Objects;

    private async void Start()
    {
        await Task.Delay(DelayInMilliseconds);

        foreach (var obj in Objects)
            if (obj != null)
                obj.SetActive(true);
    }
}
