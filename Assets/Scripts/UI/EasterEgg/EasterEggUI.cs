using UnityEngine;

public class EasterEggUI : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("Deactivate", 2f);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
