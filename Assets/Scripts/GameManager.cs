using System.Collections.Generic;
using System.Linq;
using MyBox;
using PFAS.Stats;
using PFAS.Utils;
using TMPro;
using UnityEngine;

namespace PFAS
{
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// A singleton instance of the GameManager class, allowing access from anywhere in the game.
        /// </summary>
        public static GameManager instance;

        /// <summary>
        /// A variable that holds an EnumArray mapping GlobalStats to corresponding float values. It is used to track and modify global statistics.
        /// </summary>
        public EnumArray<GlobalStats, float> gloablStats = new EnumArray<GlobalStats, float>();

        /// <summary>
        /// A variable that holds the number of competence points available.
        /// </summary>
        public int competencePoints = 0;

        public int money = 0;

        /// <summary>
        /// A readonly list of countries in the game.
        /// </summary>
        [ReadOnly]
        public List<Country> countries;

        [Separator("UI")]
        public TextMeshProUGUI moneyText;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.Log("Il y a plusieurs GameManager dans la scene");
                Destroy(this);
            }
            instance = this;

            countries = GameObject.FindGameObjectsWithTag("Country")
                   .Select(country => country.GetComponent<Country>())
                   .ToList();
        }

        // <summary>
        /// This function returns a list of all cities from all countries in the game.
        /// </summary>
        /// <returns>Returns a list of all City objects in the game.</returns>
        public List<City> GetAllCities() => countries.SelectMany(c => c.cities).ToList();

        public Country GetRandomCountry() => countries[Random.Range(0, countries.Count)];

        /// <summary>
        /// This function returns the cities of a specific country, identified by its name.
        /// </summary>
        /// <param name="country">The name of the country whose cities are to be retrieved.</param>
        /// <returns>Returns a list of City objects belonging to the specified country. If the country is not found, returns an empty list.</returns>
        public List<City> GetCountryCities(string country) => countries.FirstOrDefault(c => c.name == country)?.cities ?? new List<City>();

        /// <summary>
        /// This function updates the global statistics and subtracts the given cost from the competence points.
        /// </summary>
        /// <param name="p_stat">The global statistic to be updated.</param>
        /// <param name="p_amount">The amount to be added to the global statistic.</param>
        /// <param name="p_cost">The cost in competence points for updating the statistic.</param>
        public void UpdateStats(GlobalStats p_stat, float p_amount, int p_cost)
        {
            gloablStats[p_stat] += p_amount;
            competencePoints -= p_cost;
        }

        public void addMoney(int amount)
        {
            money += amount;
            moneyText.text = money.ToString();
        }
    }
}