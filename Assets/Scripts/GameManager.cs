using System.Collections.Generic;
using UnityEngine;

namespace PFAS
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public float regulation = 0;
        public float technologie = 0;
        public float prevention = 0;
        public float globalPollution = 0;

        public int competencePoints = 0;

        public List<City> cities;

        private void Awake()
        {
            instance = this;
            cities.ForEach(city => city.Setup());
        }

        public void UpdateStats(float p_regulation, float p_technologie, float p_prevention, float p_globalPollution, int cost)
        {
            regulation += p_regulation;
            technologie += p_technologie;
            prevention += p_prevention;
            globalPollution += p_globalPollution;
            competencePoints -= cost;
        }
    }
}