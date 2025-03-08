using System;
using MyBox;
using UnityEngine;

namespace PFAS
{
    [Serializable]
    public class City
    {
        public string name;

        [field: SerializeField, ReadOnly]
        public float vulnerability { get; private set; }

        [field: SerializeField, ReadOnly]
        public float socialResilience { get; private set; }

        [field: SerializeField, ReadOnly]
        public float adaptability { get; private set; }

        public void Setup()
        {
            vulnerability    = UnityEngine.Random.Range(0, 50);
            socialResilience = UnityEngine.Random.Range(0, 50);
            adaptability     = UnityEngine.Random.Range(0, 50);
        }

        public float ComputeScore(float p_regulation, float p_technologie, float p_prevention)
        {
            return vulnerability - ((p_regulation + p_technologie + p_prevention) / 3) - ((socialResilience + adaptability) / 2);
        }

        public void OnEvent(float p_vulnerability, float p_socialResilience, float p_adaptability)
        {
            vulnerability    += p_vulnerability;
            socialResilience += p_socialResilience;
            adaptability     += p_adaptability;
        }
    }   
}