using MyBox;
using PFAS.Stats;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using PFAS.Utils;
using Unity.Collections.LowLevel.Unsafe;

namespace PFAS.EventSystem.Events
{
    public class CityEvent : IEvent
    {
        [Separator]
        [Header("City")]
        public EnumArray<CityStats, float> statsToChange = new EnumArray<CityStats, float>();
        public bool asCityCondition = false;
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
            if (!asCityCondition) return GameManager.instance.cities[UnityEngine.Random.Range(0, GameManager.instance.cities.Count)];
            List<City> t_cities = new List<City>();

            foreach (var city in GameManager.instance.cities)
            {
                int t_correctValue = 0;
                int t_maxValue = 0;

                foreach (CityStats type in Enum.GetValues(typeof(CityStats)))
                {
                    // Check max stats condition
                    if (maxStats[type] <= 0)
                    {
                        t_maxValue++;
                        if (city.stats[type] <= maxStats[type])
                            t_correctValue++;
                    }

                    // Check min stats condition
                    if (minStats[type] <= 0)
                    {
                        t_maxValue++;
                        if (city.stats[type] >= minStats[type])
                            t_correctValue++;
                    }
                }

                // If correct values are greater than or equal to max values, add the city
                if (t_correctValue >= t_maxValue)
                    t_cities.Add(city);
            }

            if (t_cities.Count == 0) return null;

            return t_cities[UnityEngine.Random.Range(0, t_cities.Count)];
        }

        public void Use()
        {
            _city = GetRandomCity();
            if(_city == null) return;

            foreach (CityStats stat in Enum.GetValues(typeof(CityStats)))
            {
                _city.OnEvent(stat, statsToChange[stat]);
            }
        }
    }
}
