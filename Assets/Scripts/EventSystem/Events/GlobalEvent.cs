using System;
using System.Collections.Generic;
using System.Linq;
using MyBox;
using PFAS.Stats;
using PFAS.Utils;
using UnityEngine;

namespace PFAS.SystemEvent.Events
{
    public class GlobalEvent : IEvent
    {
        [Separator]
        public EnumArray<GlobalStats, float> statsToChange = new EnumArray<GlobalStats, float>();

        public bool CanUse()
        {
            return true;
        }

        public void Use()
        {
            Enum.GetValues(typeof(GlobalStats))
                .Cast<GlobalStats>()
                .Where(stat => statsToChange[stat] != 0)
                .ToList()
                .ForEach(stat => GameManager.instance.UpdateStats(stat, statsToChange[stat], 0));
        }

        public override string ToString()
        {
            var changes = new List<string>();

            // Parcourt toutes les statistiques et ajoute celles qui ont un montant non nul
            foreach (GlobalStats stat in Enum.GetValues(typeof(GlobalStats)))
            {
                float amount = statsToChange[stat];
                if (amount != 0)
                {
                    changes.Add($"{amount} en {stat}");
                }
            }

            return changes.Count > 0
                ? $"Le monde gagne {string.Join(", ", changes)}"
                : $"Le monde ne change pas.";
        }
    }
}
