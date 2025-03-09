using System.Collections.Generic;
using System.Linq;
using PFAS.Stats;
using UnityEngine;

namespace PFAS
{
    public class Country : MonoBehaviour
    {
        /// <summary>
        /// A property that holds a list of cities. The list is only accessible for reading outside the class, but can be modified within the class.
        /// </summary>
        [field: SerializeField]
        public List<City> cities { get; private set; }

        /// <summary>
        /// This function is called when the object is initialized. It sets up each city by calling the Setup method for every city in the cities list.
        /// </summary>

        private void Awake()
        {
            cities.ForEach(city => city.Setup());
        }

        /// <summary>
        /// This function calculates the sum of a specific statistic across all cities based on the given city stats.
        /// </summary>
        /// <param name="p_stats">The CityStats object that contains the specific statistics to be retrieved for each city.</param> 
        /// <returns>Returns a float value representing the sum of the specified statistic across all cities.</returns>

        public float GetStat(CityStats p_stats) => cities.Sum(city => city.GetStat(p_stats));


        /// <summary>
        /// This function handles an event for all cities, applying a specified statistic change to each city.
        /// If the divide flag is true, the amount is divided by the number of cities before applying the event.
        /// </summary>
        /// <param name="p_stat">The CityStats object that defines the statistic to be affected by the event.</param>
        /// <param name="p_amout">The amount of change to apply to the statistic.</param>
        /// <param name="p_divide">Optional parameter to determine if the amount should be divided by the number of cities (default is true).</param>

        public void OnEvent(CityStats p_stat, float p_amout, bool p_divide = true)
        {
            float t_amout = p_amout;
            if (p_divide) t_amout = p_amout / cities.Count;

            cities.ForEach(city => city.OnEvent(p_stat, t_amout));
        }
    }
}