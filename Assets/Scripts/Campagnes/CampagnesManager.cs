using System.Collections.Generic;
using MyBox;
using PFAS.Stats;
using PFAS.Timer;
using UnityEngine;

namespace PFAS.Campagnes
{
    public class CampagnesManager : MonoBehaviour
    {
        [ReadOnly]
        public List<Campagne> campagnes;

        private void OnEnable()
        {
            TimerManager.OnTick += OnDay;
        }

        private void OnDisable()
        {
            TimerManager.OnTick -= OnDay;
        }

        public void OnDay()
        {
            campagnes.ForEach(c => {
                c.timeBeforeFinish--;
                if(c.timeBeforeFinish <= 0)
                {
                    c.city.OnEvent(c.stats, c.amount, SystemEvent.TypeEvent.Campagne);
                }
            });
            campagnes.RemoveAll(c => c.timeBeforeFinish <= 0);
        }

    }

    [System.Serializable]
    public class Campagne
    {
        public CityStats stats;
        public City city;
        public int amount;
        public int timeBeforeFinish;
        public int initialTime { get; private set; }

        public Campagne(CityStats t_stats, City t_city, int t_amount, int t_timeBeforeFinish)
        {
            stats = t_stats;
            city = t_city;
            amount = t_amount;
            timeBeforeFinish = t_timeBeforeFinish;
            initialTime = t_timeBeforeFinish;
        }
    }
}
