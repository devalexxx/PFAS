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

        int _cityID;

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
                ? $"La ville {GameManager.instance.cities[_cityID].name} gagne {string.Join(", ", changes)}"
                : $"La ville {GameManager.instance.cities[_cityID].name} ne gagne rien.";
        }


        public void Use()
        {
            _cityID = UnityEngine.Random.Range(0, GameManager.instance.cities.Count);

            City t_city = GameManager.instance.cities[_cityID];

            foreach (CityStats stat in Enum.GetValues(typeof(CityStats)))
            {
                t_city.OnEvent(stat, statsToChange[stat]);
            }
        }
    }
}
