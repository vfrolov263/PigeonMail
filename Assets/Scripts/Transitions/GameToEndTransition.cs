using System.Collections.Generic;
using UnityEngine;

namespace PigeonMail
{
    public class GameToEndTransition : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> _disableList;

        private void OnEnable()
        {
            foreach (var i in _disableList)
                i.SetActive(false);
        }
    }
}