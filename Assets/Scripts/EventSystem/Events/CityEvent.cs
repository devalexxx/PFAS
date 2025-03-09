using MyBox;
using PFAS.Stats;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using PFAS.Utils;

namespace PFAS.EventSystem.Events
{
    public class CityEvent : IEvent
    {
        [Separator]
        [Header("City")]
        public EnumArray<CityStats, float> statsToChange = new EnumArray<CityStats, float>();
        public bool asCityCondition = false;
        [ConditionalField(nameof(asCityCondition))] public bool onlyOnCapital = false;
        [ConditionalField(nameof(asCityCondition))] public EnumArray<CityStats, float> maxStats = new EnumArray<CityStats, float>();
        [ConditionalField(nameof(asCityCondition))] public EnumArray<CityStats, float> minStats = new EnumArray<CityStats, float>();


        City _city;

        public bool CanUse()
        {
            return true;
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
                ? $"La ville {_city.name} gagne {string.Join(", ", changes)}"
                : $"La ville {_city.name} ne gagne rien.";
        }

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
