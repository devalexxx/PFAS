using MyBox;
using PFAS.Stats;
using UnityEngine;

namespace PFAS.EventSystem.Events
{
    public class CityEvent : IEvent
    {
        [Separator]
        [Header("City")]
        public CityStats statesToChange;
        public float amount;

        int _cityID;


        public bool CanUse()
        {
            return true;
        }

        public override string ToString()
        {
            return $"La ville {GameManager.instance.cities[_cityID].name} gagne {amount} en {statesToChange.ToString()}";
        }

        public void Use()
        {
            _cityID = Random.Range(0, GameManager.instance.cities.Count);

            City t_city = GameManager.instance.cities[_cityID];

            switch (statesToChange)
            {
                case CityStats.Adaptability: t_city.OnEvent(0, 0, amount); break;
                case CityStats.Vulnerability: t_city.OnEvent(amount, 0, 0); break;
                case CityStats.SocialResilience: t_city.OnEvent(0, amount, 0); break;
            }
        }
    }
}
