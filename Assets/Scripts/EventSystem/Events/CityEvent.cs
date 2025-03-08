using MyBox;
using PFAS.Stats;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

namespace PFAS.EventSystem.Events
{
    public class CityEvent : IEvent
    {
        [Separator]
        [Header("City")]
        public List<CityStatsForEvent> statsToChange = new List<CityStatsForEvent>();

        int _cityID;

        // Appelé automatiquement lors de la modification de l'objet dans l'éditeur
        private void OnValidate()
        {
            // S'assurer que la liste est correctement initialisée lorsque l'éditeur charge l'objet
            InitializeStatsToChange();
        }

        // Cette méthode est appelée pour initialiser ou ajouter des changements pour chaque CityStats.
        public void InitializeStatsToChange()
        {
            // Si la liste est déjà initialisée, ne rien faire.
            if (statsToChange.Count > 0)
                return;

            // Initialisation de la liste avec les valeurs par défaut.
            foreach (CityStats stat in Enum.GetValues(typeof(CityStats)))
            {
                statsToChange.Add(new CityStatsForEvent(stat, 0)); // Initialisation à 0, tu peux ajuster selon la logique.
            }
        }

        public bool CanUse()
        {
            return true;
        }

        public override string ToString()
        {
            var changes = new List<string>();

            changes.AddRange(statsToChange
                .Where(stats => stats.amount != 0) // Exclure les stats avec un montant de 0
                .Select(stats => $"{stats.amount} en {stats.stateToChange}"));

            return changes.Count > 0
                ? $"La ville {GameManager.instance.cities[_cityID].name} gagne {string.Join(", ", changes)}"
                : $"La ville {GameManager.instance.cities[_cityID].name} ne gagne rien.";
        }

        public void Use()
        {
            _cityID = UnityEngine.Random.Range(0, GameManager.instance.cities.Count);

            City t_city = GameManager.instance.cities[_cityID];

            foreach (var stats in statsToChange)
            {
                if (Enum.IsDefined(typeof(CityStats), stats.stateToChange))
                {
                    t_city.OnEvent(stats.stateToChange, stats.amount);
                }
            }
        }
    }
}
