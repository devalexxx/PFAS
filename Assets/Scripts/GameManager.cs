using System.Collections.Generic;
using System.Linq;
using MyBox;
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

        [ReadOnly]
        public List<Country> countries;

        private void Awake()
        {
            cities = new List<City>();
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

            cities.AddRange(countries.SelectMany(country => country.cities));
        }

        public void UpdateStats(float p_regulation, float p_technologie, float p_prevention, float p_globalPollution, int cost)
        {
            regulation += p_regulation;
            technologie += p_technologie;
            prevention += p_prevention;
            globalPollution += p_globalPollution;
            competencePoints -= cost;
        }

        public List<City> GetAllCities() => countries.SelectMany(c => c.cities).ToList();
        public List<City> GetCountryCities(string country) => countries.FirstOrDefault(c => c.name == country)?.cities ?? new List<City>();
    }
}