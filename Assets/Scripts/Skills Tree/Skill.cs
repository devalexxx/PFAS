using UnityEngine;

namespace PFAS.Gameplay
{
    public class Skill : MonoBehaviour
    {
        public string name { get; private set; }
        public string description { get; private set; }
        public int cost { get; private set; }
        public int regulation { get; private set; }
        public int technologie { get; private set; }
        public int prevention { get; private set; }
        public int globalPollution { get; private set; }


        public Skill(string p_name, string p_description, int p_cost, int p_regulation, int p_technologie, int p_prevention, int p_globalPollution)
        {
            name = p_name;
            description = p_description;
            cost = p_cost;
            regulation = p_regulation;
            technologie = p_technologie;
            prevention = p_prevention;
            globalPollution = p_globalPollution;
        }
    }
}
