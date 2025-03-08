using System.Collections.Generic;
using UnityEngine;

namespace PFAS
{
    public class Country : MonoBehaviour
    {
        [field: SerializeField]
        public string name { get; private set; }

        [field: SerializeField]
        public List<City> cities { get; private set; }

        private void Awake()
        {
            cities.ForEach(city => city.Setup());
        }
    }
}