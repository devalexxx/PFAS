using System;
using MyBox;
using UnityEngine;
using System.Collections.Generic;
using PFAS.Stats;
using PFAS.Utils;

namespace PFAS
{
    [Serializable]
    public class City
    {
        public string name;

        [ReadOnly]
        // Dictionnaire pour stocker les statistiques de la ville
        public EnumArray<CityStats, float> stats = new EnumArray<CityStats, float>();

        public void Setup()
        {
            // Initialisation des statistiques avec des valeurs aléatoires en utilisant foreach
            foreach (CityStats stat in Enum.GetValues(typeof(CityStats)))
            {
                stats[stat] = UnityEngine.Random.Range(0, 50);
            }
        }


        public float ComputeScore(float p_regulation, float p_technologie, float p_prevention)
        {
            return stats[CityStats.Vulnerability] - ((p_regulation + p_technologie + p_prevention) / 3) - ((stats[CityStats.SocialResilience] + stats[CityStats.Adaptability]) / 2);
        }

        // Méthode OnEvent généralisée pour les statistiques dans le dictionnaire
        public void OnEvent(CityStats statToChange, float amount)
        {
            stats[statToChange] += amount;
        }

        // Accès aux valeurs des statistiques via un getter
        public float GetStat(CityStats stat)
        {
            return stats[stat];
        }
    }
}
