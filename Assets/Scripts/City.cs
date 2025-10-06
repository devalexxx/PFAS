using System;
using PFAS.Stats;
using PFAS.Utils;
using MyBox;
using UnityEngine;
using PFAS.SystemEvent;

namespace PFAS
{
    [Serializable]
    public class City
    {
        /// <summary>
        /// The name of the city.
        /// </summary>
        public string name;

        /// <summary>
        /// A boolean that indicates if the city is the capital city.
        /// </summary>
        public bool isCapital;

        /// <summary>
        /// A property that holds the name of the country to which the city belongs.
        /// It is a read-only property, with the value being set through the Setup method.
        /// </summary>
        public string country {  get; private set; }

        /// <summary>
        /// A dictionary to store the city's statistics, such as vulnerability, social resilience, etc.
        /// </summary>
        public EnumArray<CityStats, float> stats = new EnumArray<CityStats, float>(() => 0f);

        [SerializeField]
        private float _currentContamination;
        public float currentContamination
        {
            get => _currentContamination;
            set => _currentContamination = Mathf.Max(0, Mathf.Min(100f, value));
        }

        /// <summary>
        /// Default constructor to initialize the city's statistics to 0.
        /// </summary>
        public City()
        {
            stats = new EnumArray<CityStats, float>(() => 0f);
        }

        /// <summary>
        /// This function sets up the city with random statistics and assigns it to a specified country.
        /// </summary>
        /// <param name="p_countryName">The name of the country to which this city belongs.</param>

        public void Setup(string p_countryName)
        {
            stats = new EnumArray<CityStats, float>(() => UnityEngine.Random.Range(0, 25));
            _currentContamination = UnityEngine.Random.Range(0, 10);
            stats[CityStats.Vulnerability] = UnityEngine.Random.Range(50, 75);
            country = p_countryName;
        }

        /// <summary>
        /// This function calculates the city's vulnerability score based on various parameters.
        /// </summary>
        /// <param name="p_regulation">The regulation score to be used in the calculation.</param>
        /// <param name="p_technologie">The technology score to be used in the calculation.</param>
        /// <param name="p_prevention">The prevention score to be used in the calculation.</param>
        /// <returns>Returns the calculated score of the city based on its statistics and input parameters.</returns>
        public float ComputeScore(float p_regulation, float p_technologie, float p_prevention)
        {
            return stats[CityStats.Vulnerability] - ((p_regulation + p_technologie + p_prevention) / 3) - ((stats[CityStats.SocialResilience] + stats[CityStats.Adaptability]) / 2);
        }

        /// <summary>
        /// This function is used to modify a specific city's statistic by a certain amount.
        /// </summary>
        /// <param name="p_statToChange">The statistic to be modified.</param>
        /// <param name="p_amount">The amount to change the statistic by.</param>
        public void OnEvent(CityStats p_statToChange, float p_amount, TypeEvent p_eventType)
        {
            stats[p_statToChange] += p_amount;
        }

        /// <summary>
        /// This function returns the value of a specific statistic for the city.
        /// </summary>
        /// <param name="p_stat">The specific statistic to retrieve.</param>
        /// <returns>Returns the value of the specified statistic.</returns>
        public float GetStat(CityStats p_stat)
        {
            return stats[p_stat];
        }

        public void propagation(float MoyenneVoisines)
        {
            currentContamination += (GameManager.BASEGROWTH * stats[CityStats.Vulnerability]) - (GameManager.RESISTFACTOR * stats[CityStats.SocialResilience])
                - (GameManager.ADAPTFACTOR * stats[CityStats.Adaptability]) + GameManager.OTHERFACOTR * (MoyenneVoisines - currentContamination);
        }
    }
}
