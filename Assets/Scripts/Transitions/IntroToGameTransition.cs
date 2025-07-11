using UnityEngine;

public class IntroToGameTransition : MonoBehaviour
{
    [SerializeField]
    private GameObject _input, _tutorial;

    void OnEnable()
    {
        _input.SetActive(true);
        _tutorial.SetActive(true);
        Destroy(transform.parent.parent.gameObject);
    }
}
