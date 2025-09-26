using System.Collections.Generic;
using System.Linq;
using PFAS.Map;
using PFAS.Stats;
using PFAS.SystemEvent;
using PFAS.Timer;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PFAS
{
    public class Country : MonoBehaviour, IPointerClickHandler
    {
        /// <summary>
        /// A property that holds a list of cities. The list is only accessible for reading outside the class, but can be modified within the class.
        /// </summary>
        [field: SerializeField]
        public List<City> cities { get; private set; }

        MapInputManager _map;
        private SpriteRenderer        _mask;
        private MaterialPropertyBlock _blockProps;

        public float globalContamination => cities.Sum(city => city.currentContamination);

        public List<Country> neighbourCountries = new List<Country>();

        private Material _mat;

        /// <summary>
        /// This function is called when the object is initialized. It sets up each city by calling the Setup method for every city in the cities list.
        /// </summary>

        private void Awake()
        {
            cities.ForEach(city => city.Setup(name));
            _blockProps = new();
            
            _mask = GetComponent<SpriteRenderer>();
            _mat = GetComponent<Renderer>().material;
        }

        private void Update()
        {
            //_blockProps.SetFloat("_Spread", cities.Sum(t_city => t_city.currentContamination) / (100f * cities.Count));
            //_mask.SetPropertyBlock(_blockProps);
        }

        public void OnDay()
        {
            cities.ForEach((city) =>
            {
                float otherfactor = cities.Where(c => c.name != city.name).Sum(city => city.currentContamination);
                city.propagation(otherfactor / cities.Count - 1);
            });

            neighbourCountries.ForEach((country) => 
            {
                country.SpreadPollution(GameManager.OTHERFACOTR * (globalContamination - country.globalContamination));
            });

            _mat.SetFloat("_Progress", globalContamination / 100);
        }

        private void Start()
        {
            _map = GameManager.instance.GetComponent<MapInputManager>();
            TimerManager.OnTick += OnDay;
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

        public void OnEvent(CityStats p_stat, float p_amout, TypeEvent p_eventType, bool p_divide = true)
        {
            float t_amout = p_amout;
            if (p_divide) t_amout = p_amout / cities.Count;

            cities.ForEach(city => city.OnEvent(p_stat, t_amout, p_eventType));
        }

        public void SpreadPollution(float p_pollution)
        {
            cities.ForEach(city => city.currentContamination += p_pollution / cities.Count);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            
        }
    }
}