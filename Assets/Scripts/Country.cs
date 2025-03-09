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

    }
}