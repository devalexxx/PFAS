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
        public bool divideForEachCity = true;
        [Separator]
        public bool asCountryCondition = false;
        [ConditionalField(nameof(asCountryCondition))] public int maxCity, minCity = -1;
        [ConditionalField(nameof(asCountryCondition))] public EnumArray<CityStats, float> maxStats = new EnumArray<CityStats, float>();
        [ConditionalField(nameof(asCountryCondition))] public EnumArray<CityStats, float> minStats = new EnumArray<CityStats, float>();

        Country _country;

        public bool CanUse()
        {
            return true;
        }

        public Country GetCountry()
        {
            List<Country> t_countries = GameManager.instance.countries;

            if (!asCountryCondition)
            {
                return t_countries[UnityEngine.Random.Range(0, t_countries.Count)];
            }

            var t_valideCountries = t_countries.Where(country =>
            {
                int t_currentValue = 0, t_maxValue = 0;

                // Check city count conditions
                if (maxCity > 0)
                {
                    t_maxValue++;
                    if (country.cities.Count <= maxCity) t_currentValue++;
                }

                if (minCity > 0)
                {
                    t_maxValue++;
                    if (country.cities.Count >= minCity) t_currentValue++;
                }

                // Check stat conditions
                foreach (CityStats type in Enum.GetValues(typeof(CityStats)))
                {
                    t_maxValue += 2;
                    if (country.GetStat(type) <= maxStats[type]) t_currentValue++;
                    if (country.GetStat(type) >= minStats[type]) t_currentValue++;
                }

                return t_currentValue == t_maxValue;
            }).ToList();

            // Return a random valid country or fallback to all countries
            return t_valideCountries.Any()
                ? t_valideCountries[UnityEngine.Random.Range(0, t_valideCountries.Count)]
                : t_countries[UnityEngine.Random.Range(0, t_countries.Count)];
        }

        public void Use()
        {
            _country = GetCountry();
            if (_country == null) return;

            Enum.GetValues(typeof(CityStats))
                .Cast<CityStats>()
                .Where(stat => statsToChange[stat] != 0)
                .ToList()
                .ForEach(stat => _country.OnEvent(stat, statsToChange[stat], divideForEachCity));
        }


        public override string ToString()
        {
            var changes = new List<string>();

            // Parcourt toutes les statistiques et ajoute celles qui ont un montant non nul
            foreach (CityStats stat in Enum.GetValues(typeof(CityStats)))
            {
                float amount = statsToChange[stat];
                if (amount != 0)
                {
                    changes.Add($"{amount} en {stat}");
                }
            }

            return changes.Count > 0
                ? $"Le pays {_country.name} gagne {string.Join(", ", changes)}"
                : $"Le pays {_country.name} ne gagne rien.";
        }
    }
}
