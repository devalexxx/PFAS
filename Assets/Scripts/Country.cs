using System;
using System.Collections.Generic;
using System.Linq;
using PFAS.Stats;
using UnityEngine;

namespace PFAS
{
    public class Country : MonoBehaviour
    {
        [field: SerializeField]
        public List<City> cities { get; private set; }

        private void Awake()
        {
            cities.ForEach(city => city.Setup());
        }

        public float GetStat(CityStats p_stats) => cities.Sum(city => city.GetStat(p_stats));

        public void OnEvent(CityStats p_stat, float p_amout, bool p_divide = true)
        {
            float t_amout = p_amout;
            if (p_divide) t_amout = p_amout / cities.Count;

            cities.ForEach(city => city.OnEvent(p_stat, t_amout));
        }
    }
}