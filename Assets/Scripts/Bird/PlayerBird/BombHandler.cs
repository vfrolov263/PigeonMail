using UnityEngine;

namespace PigeonMail
{
    public class BombHandler : MonoBehaviour
    {
        [SerializeField]
        GameObject _bombPrefab;
        [SerializeField]
        private float _timeForSpawnBomb = 4f;
        private bool _readyForDrop = true;

        public void Drop()
        {
            if (_readyForDrop)
            {
                Destroy(Instantiate(_bombPrefab, transform.position, Quaternion.identity), _timeForSpawnBomb);
                _readyForDrop = false;
                Invoke("Reload", _timeForSpawnBomb);
            }
        }

        private void Reload()
        {
            _readyForDrop = true;
        }
    }
}
