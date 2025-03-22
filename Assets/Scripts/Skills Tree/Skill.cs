using System;
using PFAS.Stats;
using PFAS.Utils;
using UnityEngine;

namespace PFAS.Gameplay
{
    public class Skill : MonoBehaviour
    {
        public string description;
        public int cost;
        public EnumArray<GlobalStats, float> stats;
        public Skill[] previous;
        public Skill[] after;
        public bool unlocked;
        public bool purchased = false;

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

        public bool BuySkill()
        {
            if (purchased)
                return true;
            if (unlocked)
            {
                if (GameManager.instance.competencePoints >= cost)
                {
                    
                    GameManager.instance.UpdateStats(stats, cost);
                    purchased = true;
                    foreach (Skill skill in after)
                    {
                        skill.CheckUnlockSkill();
                    }
                    return true;
                }
            }
            return false;
        }
        public void CheckUnlockSkill()
        {
            if (previous.Length == 0)
            {
                unlocked = true;
            }
            else
            {
                bool isUnlockable = true;
                foreach (Skill skill in previous)
                {
                    if (!skill.purchased)
                    {
                        isUnlockable = false;
                    }
                }
                unlocked = isUnlockable;
            }
        }
    }
}
