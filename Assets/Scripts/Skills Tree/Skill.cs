using UnityEngine;

namespace PFAS.Gameplay
{
    public class Skill : MonoBehaviour
    {
        public string name { get; private set; }
        public string description { get; private set; }
        public int cost { get; private set; }
        public float regulation { get; private set; }
        public float technologie { get; private set; }
        public float prevention { get; private set; }
        public float globalPollution { get; private set; }
        public Skill[] after { get; private set; }
        public Skill[] previous { get; private set; }
        public bool unlocked { get; private set; }
        public bool purchased { get; private set; } = false;
        public Skill(string p_name, string p_description, int p_cost, float p_regulation, float p_technologie, float p_prevention, float p_globalPollution, bool p_unlocked, Skill[] p_after = default(Skill[]), Skill[] p_previous = default(Skill[]))
        {
            name = p_name;
            description = p_description;
            cost = p_cost;
            regulation = p_regulation;
            technologie = p_technologie;
            prevention = p_prevention;
            globalPollution = p_globalPollution;
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
                    GameManager.instance.UpdateStats(regulation, technologie, prevention, globalPollution, cost);
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
