using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace PigeonMail
{
    public class SlideFlipper : MonoBehaviour
    {
        [SerializeField]
        private GameObject _slideContainer;
        private Queue<GameObject> _slides = new();

        private void Awake()
        {
            for (int i = 0; i < _slideContainer.transform.childCount; i++)
            {
                _slides.Enqueue(_slideContainer.transform.GetChild(i).gameObject);
            }
            // _slides = new(System.Array.ConvertAll<Transform, GameObject>(
            //     _slideContainer.GetComponentsInChildren<Transform>(), t => t.gameObject));

            //_slides.Dequeue();
        }

        private void Start()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;   
        }

        public void NextSlide()
        {
            Destroy(_slides.Dequeue());
            _slides.Peek().SetActive(true);
        }
    }
}