using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public class SetAudioVolumeOnAwake : MonoBehaviour
    {
        [Inject]
        public void Construct(AudioPlayer.Settings settings) => GetComponent<AudioSource>().volume = settings.volume;

        private void Awake()
        {
            
        }
    }
}