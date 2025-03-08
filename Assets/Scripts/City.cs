using System;
using MyBox;
using UnityEngine;
using System.Collections.Generic;
using PFAS.Stats;

namespace PFAS
{
    [Serializable]
    public class City
    {
        public string name;

        // Dictionnaire pour stocker les statistiques de la ville
        private Dictionary<CityStats, float> stats = new Dictionary<CityStats, float>();

        public void Setup()
        {
            // Initialisation des statistiques avec des valeurs aléatoires
            stats[CityStats.Vulnerability] = UnityEngine.Random.Range(0, 50);
            stats[CityStats.SocialResilience] = UnityEngine.Random.Range(0, 50);
            stats[CityStats.Adaptability] = UnityEngine.Random.Range(0, 50);
        }

        public float ComputeScore(float p_regulation, float p_technologie, float p_prevention)
        {
            return stats[CityStats.Vulnerability] - ((p_regulation + p_technologie + p_prevention) / 3) - ((stats[CityStats.SocialResilience] + stats[CityStats.Adaptability]) / 2);
        }

        // Méthode OnEvent généralisée pour les statistiques dans le dictionnaire
        public void OnEvent(CityStats statToChange, float amount)
        {
            if (stats.ContainsKey(statToChange))
            {
                stats[statToChange] += amount;
            }
            else
            {
                Debug.LogWarning($"Statistique {statToChange} non trouvée dans la ville.");
            }
            DebugStats();
        }

        public void DebugStats()
        {
            foreach (var stat in stats)
            {
                Debug.Log($"Statistique: {stat.Key}, Valeur: {stat.Value}");
            }
        }

        // Accès aux valeurs des statistiques via un getter
        public float GetStat(CityStats stat)
        {
            return stats.ContainsKey(stat) ? stats[stat] : 0f;
        }
    }
}
