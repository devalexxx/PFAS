using System;
using PFAS.Stats;
using PFAS.Utils;
using UnityEngine;

namespace PFAS.Gameplay
{
    public class Skill : MonoBehaviour
    {
        public string name { get; private set; }
        public string description { get; private set; }
        public int cost { get; private set; }
        public EnumArray<GlobalStats, float> stats;
        public Skill[] after { get; private set; }
        public Skill[] previous { get; private set; }
        public bool unlocked { get; private set; }
        public bool purchased { get; private set; } = false;

        public Skill(string p_name, string p_description, int p_cost, float p_regulation, float p_technologie, float p_prevention, float p_globalPollution, bool p_unlocked, Skill[] p_after = default(Skill[]), Skill[] p_previous = default(Skill[]))
        {
            name = p_name;
            description = p_description;
            cost = p_cost;
            stats = new EnumArray<GlobalStats, float>();
            stats[GlobalStats.Prevention] = p_prevention;
            stats[GlobalStats.Regulation] = p_regulation;
            stats[GlobalStats.Technologie] = p_technologie;
            stats[GlobalStats.GlobalPollution] = p_globalPollution;
            unlocked = p_unlocked;
            after = p_after;
            previous = p_previous;
        }

        public void BuySkill(int competencePoints)
        {
            if (competencePoints >= cost)
            {
                if (unlocked)
                {
                    foreach (GlobalStats currentStat in Enum.GetValues(typeof(GlobalStats)))
                    {
                        GameManager.instance.UpdateStats(currentStat, stats[currentStat], cost);
                    }
                    purchased = true;
                    foreach (Skill skill in after)
                    {
                        skill.CheckUnlockSkill();
                    }
                }
            }
        }
        public void CheckUnlockSkill()
        {
            if (previous.Length == 0)
            {
                unlocked = true;
            }
            else
            {
                foreach (Skill skill in previous)
                {
                    if (skill.purchased)
                    {
                        unlocked = true;
                    }
                }
            }
        }
    }
}
