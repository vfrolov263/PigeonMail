using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Settings;

namespace PigeonMail
{
    public class TextPrinter : MonoBehaviour
    {
        [SerializeField]
        private string _table, _key;
        public UnityEvent _onEndPrintActions;
        private TMP_Text _text;
        private string _fullText;
        private float _timeOnChar = 0.04f;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            _fullText = LocalizationSettings.StringDatabase.GetLocalizedString(_table, _key);
        }

        private void Start()
        {
            StartCoroutine(Print());
        }

        private IEnumerator Print()
        {
            foreach (char c in _fullText)
            {
                _text.text += c;
                yield return new WaitForSeconds(_timeOnChar);
            }

            _onEndPrintActions?.Invoke();
        }

        private void Update()
        {
            if (Input.anyKey)
            {
                StopCoroutine(Print());
                _text.text = _fullText;
                _onEndPrintActions?.Invoke();
                Destroy(this);
            } 
        }
    }
}