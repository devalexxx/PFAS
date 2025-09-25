using System;
using PFAS.Stats;
using PFAS.Utils;
using UnityEngine;

namespace PFAS.Gameplay
{
    [System.Serializable]
    public class Skill
    {
        public string name { get; private set; }
        public string description { get; private set; }
        public int cost { get; private set; }
        public EnumArray<GlobalStats, float> stats;
        public Skill[] after { get; set; }
        [SerializeField]
        public Skill[] previous { get; set; }
        public bool unlocked { get; private set; }
        public bool purchased { get; private set; } = false;

        public Vector2 pos = Vector2.zero;

        public Skill(string p_name, string p_description, int p_cost, float p_regulation, float p_technologie, float p_prevention, float p_globalPollution, bool p_unlocked, Vector2 p_pos = default, Skill[] p_after = default(Skill[]), Skill[] p_previous = default(Skill[]))
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
            this.after = p_after ?? new Skill[0];
            this.previous = p_previous ?? new Skill[0];
            pos = p_pos;
        }

        public bool BuySkill()
        {
            if (GameManager.instance.money >= cost)
            {
                if (unlocked)
                {
                    foreach (GlobalStats currentStat in Enum.GetValues(typeof(GlobalStats)))
                    {
                        GameManager.instance.UpdateStats(currentStat, stats[currentStat]);
                    }
                    GameManager.instance.money -= cost;

                    GameManager.instance.moneyText.text = GameManager.instance.money.ToString();

                    purchased = true;
                    foreach (Skill skill in after)
                    {
                        skill.CheckUnlockSkill();
                    }
                }
            }

            return purchased;
        }

        public void CheckUnlockSkill()
        {
            if (previous == null) return;
            if (previous.Length == 0)
            {
                unlocked = true;
            }
            else
            {
                var i = 0;
                foreach (Skill skill in previous)
                {
                    if (skill.purchased)
                    {
                        i++;
                    }
                }

                if(i == previous.Length) { unlocked = true; }
            }
        }
    }
}
