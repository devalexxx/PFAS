using System;
using System.Collections.Generic;
using System.Linq;
using MyBox;
using PFAS.Stats;
using PFAS.Utils;
using UnityEngine;


namespace PFAS.EventSystem.Events
{
    public class CountryEvent : IEvent
    {
        [Separator]
        [Header("Country")]
        public EnumArray<CityStats, float> statsToChange = new EnumArray<CityStats, float>();
        public bool asCountryCondition = false;

        Country _country;

        public bool CanUse()
        {
            return true;
        }

        public Country GetCountry()
        {
            List<Country> t_countries = GameManager.instance.countries;
            if (asCountryCondition)
            {
                return null;
            }
            else
            {
                return t_countries[UnityEngine.Random.Range(0, t_countries.Count)];
            }
        }

        public void Use()
        {
            _country = GetCountry();
            if (_country == null) return;

            Enum.GetValues(typeof(CityStats))
                .Cast<CityStats>()
                .Where(stat => statsToChange[stat] != 0)
                .ToList()
                .ForEach(stat => _country.OnEvent(stat, statsToChange[stat]));
        }
        
    }
}
