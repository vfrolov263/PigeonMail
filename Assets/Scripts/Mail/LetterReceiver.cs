using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public class LetterReceiver : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Letter")
            {
                Letter letter = other.gameObject.GetComponent<Letter>();
                letter.Status = LetterStatus.Delivered;
                Destroy(gameObject);
            }
        }

        public class Factory : PlaceholderFactory<LetterReceiver>
        {
        }
    }
}