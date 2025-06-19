using UnityEngine;
using Zenject;

namespace PigeonMail
{
    public enum CityColor
    {
        Red,
        Blue,
        Yellow
    }

    public class Clerk : Human
    {
        [SerializeField]
        private CityColor _cityColor;

        public CityColor CityFlagColor
        {
            get
            {
                return _cityColor;
            }
        }
    }
}