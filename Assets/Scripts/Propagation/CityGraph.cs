using System;
using System.Collections.Generic;
using System.Linq;
using MyBox;
using PFAS.Stats;
using PFAS.Utils;
using UnityEngine;

namespace PFAS.Propagation
{
    [Serializable]
    public class Link
    {
        public string lhs;
        public string rhs;
        public float   factor;
    }

    [Serializable]
    public class CityGraph
    {
        [SerializeField]
        private List<string> _cities;

        [SerializeField]
        private List<Link> _links;

        private Dictionary<string, City>           _citiesResolver;
        private Dictionary<(string, string), float> _mappedLinks;
        
        public void BuildGraph(List<City> p_cities)
        {
            _citiesResolver = new();
            _cities.ForEach(t_name => {
                var t_found = p_cities.Find(t_city => t_city.name == t_name);
                if (t_found != null)
                {
                    _citiesResolver[t_name] = t_found;
                }
                else
                {
                    Debug.LogWarning($"City {t_name} can't be found in scene!");
                }
            });
            _mappedLinks = _links
                .SelectMany(t_link => new[]
                {
                    new KeyValuePair<(string, string), float>((t_link.lhs, t_link.rhs), t_link.factor),
                    new KeyValuePair<(string, string), float>((t_link.rhs, t_link.lhs), t_link.factor)
                })
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public void Propagate(EnumArray<GlobalStats, float> p_stats)
        {
            Dictionary<string, float> t_acc = new();
            _mappedLinks.ForEach(t_link => {
                t_acc.GetOrAdd(t_link.Key.Item2, _citiesResolver[t_link.Key.Item1].currentContamination * t_link.Value);
            });

            t_acc.ForEach(t_kv => {
                _citiesResolver[t_kv.Key].currentContamination += 0.05f * t_kv.Value * _citiesResolver[t_kv.Key].ComputeScore(p_stats[GlobalStats.Regulation], p_stats[GlobalStats.Technologie], p_stats[GlobalStats.Prevention]);
            });
        }

        public List<City> GetLinked(string p_city)
        {
            List<City> t_cities = new();
            _mappedLinks.ForEach(t_link => {
                if (t_link.Key.Item1 == p_city)
                    t_cities.Add(_citiesResolver[t_link.Key.Item2]);
            });

            return t_cities;
        }
    }   
}
