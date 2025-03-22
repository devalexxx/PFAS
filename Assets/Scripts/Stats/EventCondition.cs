using System;
using System.Linq;
using MyBox;
using PFAS.Timer;
using PFAS.Utils;
using Unity.VisualScripting;
using UnityEngine;

namespace PFAS.Stats
{
    [System.Serializable]
    public class EventCondition
    {
        public bool useCondition;
        [Header("Date (jj/mm/aaa)")]
        [ConditionalField(nameof(useCondition))]
        public string maxDate;
        [ConditionalField(nameof(useCondition))]
        public string minDate;
        [ConditionalField(nameof(useCondition))]
        public EnumArray<GlobalStats, float> minGlobalStats;
        [ConditionalField(nameof(useCondition))]
        public EnumArray<GlobalStats, float> maxGlobalStats;
        
        public bool CanUse(TimerManager p_time)
        {
            if(!useCondition) return true;
            bool t_canUse = true;
            if (maxDate != null)
            {
                if (DateTime.TryParseExact(maxDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime t_parsedMaxDate))
                {
                    t_canUse &= p_time.currentDate < t_parsedMaxDate;
                }
            }
            if (minDate != null)
            {
                if (DateTime.TryParseExact(maxDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime t_parsedMinDate))
                {
                    t_canUse &= p_time.currentDate > t_parsedMinDate;
                }
            }

            Enum.GetValues(typeof(GlobalStats))
                .Cast<GlobalStats>()
                .Where(stat => minGlobalStats[stat] != 0)
                .ToList()
                .ForEach(stat => t_canUse &= minGlobalStats[stat] <= GameManager.instance.globalStats[stat]);

            Enum.GetValues(typeof(GlobalStats))
                .Cast<GlobalStats>()
                .Where(stat => maxGlobalStats[stat] != 0)
                .ToList()
                .ForEach(stat => t_canUse &= maxGlobalStats[stat] >= GameManager.instance.globalStats[stat]);

            return t_canUse;
        }
    }
}
