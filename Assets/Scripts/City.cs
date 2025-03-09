using System;
using MyBox;
using PFAS.Stats;
using PFAS.Utils;
using UnityEngine;

namespace PFAS
{
    [Serializable]
    public class City
    {
        public string name;
        public bool isCapital;

        // Dictionnaire pour stocker les statistiques de la ville
        public EnumArray<CityStats, float> stats = new EnumArray<CityStats, float>(() => 0f);

        public City()
        {
            stats = new EnumArray<CityStats, float>(() => 0f); // Initialiser avec 0 par défaut
        }

        public void Setup()
        {
            stats = new EnumArray<CityStats, float>(() => UnityEngine.Random.Range(0, 50));
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
