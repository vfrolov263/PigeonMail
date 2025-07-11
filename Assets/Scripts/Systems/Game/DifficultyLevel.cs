using UnityEngine;

namespace PigeonMail
{
    [CreateAssetMenu(fileName = "DifficultyLevel", menuName = "Difficulty level")]
    public class DifficultyLevel : ScriptableObject
    {
        public float initialDeliveryTime, initialDeliveryTimeDelta, initialDeliveryTimeMin;
        public float timeBetweenHits;
    }
}