using System.Collections.Generic;
using System.Linq;
using MyBox;
using PFAS.Stats;
using PFAS.Utils;
using UnityEngine;

namespace PFAS
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public EnumArray<GlobalStats, float> gloablStats = new EnumArray<GlobalStats, float>();

        public int competencePoints = 0;

        [ReadOnly]
        public List<Country> countries;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.Log("Il y a plusieurs GameManager dans la scene");
                Destroy(this);
            }
            instance = this;
        }

        private void Start()
        {
            countries = GameObject.FindGameObjectsWithTag("Country")
                   .Select(country => country.GetComponent<Country>())
                   .ToList();
        }

        public void UpdateStats(GlobalStats p_stat, float p_amount, int p_cost)
        {
            gloablStats[p_stat] += p_amount;
            competencePoints -= p_cost;
        }

        public List<City> GetAllCities() => countries.SelectMany(c => c.cities).ToList();
        public List<City> GetCountryCities(string country) => countries.FirstOrDefault(c => c.name == country)?.cities ?? new List<City>();
    }
}