using System;
using System.Collections.Generic;
using System.Linq;
using MyBox;
using PFAS.Stats;
using PFAS.Utils;
using UnityEngine;


namespace PFAS.SystemEvent.Events
{
    public class CountryEvent : IEvent
    {
        /// <summary>
        /// A variable that holds an EnumArray mapping CityStats to corresponding float values. It is used to track and modify statistics for cities.
        /// </summary>
        [Separator]
        [Header("Country")]
        public EnumArray<CityStats, float> statsToChange = new EnumArray<CityStats, float>();

        /// <summary>
        /// A boolean flag that determines whether the stat changes should be divided equally among all cities in the country.
        /// </summary>
        public bool divideForEachCity = true;

        /// <summary>
        /// A flag indicating whether the current logic is based on country conditions.
        /// </summary>
        [Separator]
        public bool asCountryCondition = false;

        /// <summary>
        /// The maximum number of cities a country can have in order to meet the conditions.
        /// </summary>
        [ConditionalField(nameof(asCountryCondition))] public int maxCity = -1;

        /// <summary>
        /// The minimum number of cities a country can have in order to meet the conditions.
        /// </summary>
        [ConditionalField(nameof(asCountryCondition))] public int minCity = -1;

        /// <summary>
        /// A list of maximum values for different statistics that a country must have to meet the conditions.
        /// </summary>
        [ConditionalField(nameof(asCountryCondition))] public EnumArray<CityStats, float> maxStats = new EnumArray<CityStats, float>();

        /// <summary>
        /// A list of minimum values for different statistics that a country must have to meet the conditions.
        /// </summary>
        [ConditionalField(nameof(asCountryCondition))] public EnumArray<CityStats, float> minStats = new EnumArray<CityStats, float>();


        /// <summary>
        /// A private field representing the country that is currently selected or being used.
        /// </summary>
        Country _country;



        /// <summary>
        /// This function checks if the action can be performed.
        /// </summary>
        /// <returns>Returns a boolean value indicating if the action can be performed</returns>
        public bool CanUse()
        {
            return true;
        }

        /// <summary>
        /// This function selects a country based on the given conditions. If asCountryCondition is true, it filters countries based on the city count and statistics conditions.
        /// </summary>
        /// <returns>Returns a random Country object that matches the given conditions, or a fallback country if none match.</returns>
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

        /// <summary>
        /// This function applies the selected statistics changes to the chosen country.
        /// </summary>
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

        /// <summary>
        /// This function returns a string representation of the actions taken on the country, including the statistics that were changed and their values.
        /// </summary>
        /// <returns>Returns a string describing the changes made to the country's statistics.</returns>
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
