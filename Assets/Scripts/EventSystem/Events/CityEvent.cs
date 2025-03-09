using MyBox;
using PFAS.Stats;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using PFAS.Utils;

namespace PFAS.SystemEvent.Events
{
    public class CityEvent : IEvent
    {
        /// <summary>
        /// A variable that holds an EnumArray mapping CityStats to corresponding float values. It is used to track and modify statistics for cities.
        /// </summary>
        [Separator]
        [Header("City")]
        public EnumArray<CityStats, float> statsToChange = new EnumArray<CityStats, float>();

        /// <summary>
        /// A flag indicating whether the current logic is based on city conditions.
        /// </summary>
        public bool asCityCondition = false;

        /// <summary>
        /// A flag indicating whether the event should only apply to the capital city.
        /// </summary>
        [ConditionalField(nameof(asCityCondition))] public bool onlyOnCapital = false;

        /// <summary>
        /// A list of maximum values for different statistics that a city must have to meet the conditions.
        /// </summary>
        [ConditionalField(nameof(asCityCondition))] public EnumArray<CityStats, float> maxStats = new EnumArray<CityStats, float>();

        /// <summary>
        /// A list of minimum values for different statistics that a city must have to meet the conditions.
        /// </summary>
        [ConditionalField(nameof(asCityCondition))] public EnumArray<CityStats, float> minStats = new EnumArray<CityStats, float>();

        /// <summary>
        /// A private field representing the city that is currently selected or being used.
        /// </summary>
        City _city;


        /// <summary>
        /// This function checks if the event can be performed.
        /// </summary>
        /// <returns>Returns a boolean value indicating if the event can be performed/returns>
        public bool CanUse()
        {
            return true;
        }

        /// <summary>
        /// This function returns a string representation of the actions taken on the city, including the statistics that were changed and their values.
        /// </summary>
        /// <returns>Returns a string describing the changes made to the city's statistics.</returns>
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
                ? $"La ville {_city.name} gagne {string.Join(", ", changes)}"
                : $"La ville {_city.name} ne gagne rien.";
        }


        /// <summary>
        /// This function selects a random city based on the given conditions. If asCityCondition is true, it filters cities based on the city condition.
        /// </summary>
        /// <returns>Returns a random City object that matches the given conditions, or null if no city matches.</returns>
        public City GetRandomCity()
        {
            List<City> t_cities = GameManager.instance.GetAllCities();
            if (!asCityCondition)
                return t_cities[UnityEngine.Random.Range(0, t_cities.Count)];

            List<City> filteredCities = t_cities
                .Where(city => (!onlyOnCapital || city.isCapital) && MatchesStats(city))
                .ToList();

            return filteredCities.Count > 0
                ? filteredCities[UnityEngine.Random.Range(0, filteredCities.Count)]
                : null;
        }


        /// <summary>
        /// This function checks if the selected city matches the statistics conditions (max and min values) defined for the event.
        /// </summary>
        /// <param name="city">The city to be checked against the conditions.</param>
        /// <returns>Returns true if the city matches the statistics conditions, otherwise false.</returns>
        private bool MatchesStats(City city)
        {
            int correctValues = 0, maxValues = 0;

            foreach (CityStats type in Enum.GetValues(typeof(CityStats)))
            {
                if (maxStats[type] <= 0)
                {
                    maxValues++;
                    if (city.stats[type] <= maxStats[type]) correctValues++;
                }
                if (minStats[type] <= 0)
                {
                    maxValues++;
                    if (city.stats[type] >= minStats[type]) correctValues++;
                }
            }

            return correctValues >= maxValues;
        }


        /// <summary>
        /// This function applies the selected statistics changes to the chosen city.
        /// </summary>
        public void Use()
        {
            _city = GetRandomCity();
            if(_city == null) return;

            Enum.GetValues(typeof(CityStats))
                .Cast<CityStats>()
                .Where(stat => statsToChange[stat] != 0)
                .ToList()
                .ForEach(stat => _city.OnEvent(stat, statsToChange[stat]));
        }
    }
}
