using UnityEngine;

namespace PFAS.Gameplay
{
    public class TechnologicalTree : GlobalTree
    {
        private Skill[] _advancedDetectionMethods;
        private Skill[] _innovativeDecontaminationTechnologies;
        private Skill[] _chemicalAlternatives;
        private Skill[] _extensiveEpidemiologicalStudies;

        private void Start()
        {
            _advancedDetectionMethods[0] = new Skill("Environmental nanosensors", "", 1, 0, 10, 6, -5, false);
            _advancedDetectionMethods[1] = new Skill("Rapid tests", "", 2, 0, 8, 8, -4, false);
            _advancedDetectionMethods[2] = new Skill("Genetic analysis of PFAS", "", 3, 5, 12, 0, -6, false);

            _innovativeDecontaminationTechnologies[0] = new Skill("Bioremediation", "", 5, 0, 15, 0, -12, false);
            _innovativeDecontaminationTechnologies[1] = new Skill("Advanced adsorption techniques", "", 8, 0, 10, 0, -8, false);
            _innovativeDecontaminationTechnologies[2] = new Skill("Chemical destruction processes", "", 3, 0, 12, 0, -10, false);

            _chemicalAlternatives[0] = new Skill("Hydrophobic materials without PFAS", "", 4, 5, 8, 0, -6, false);
            _chemicalAlternatives[1] = new Skill("Biodegradable substitutes", "", 5, 5, 10, 0, -7, false);
            _chemicalAlternatives[2] = new Skill("Changes to production chains", "", 6, 6, 12, 0, -8, false);

            _extensiveEpidemiologicalStudies[0] = new Skill("Mapping of PFAS-related diseases", "", 7, 0, 8, 8, 5, false);
            _extensiveEpidemiologicalStudies[1] = new Skill("Advanced bioassays", "", 8, 0, 10, 10, 0, false);
            _extensiveEpidemiologicalStudies[2] = new Skill("Impact on reproduction and development", "", 9, 6, 8, 5, 0, false);

            foreach (Skill skill in _advancedDetectionMethods)
            {
                skill.CheckUnlockSkill();
            }
            foreach (Skill skill in _innovativeDecontaminationTechnologies)
            {
                skill.CheckUnlockSkill();
            }
            foreach (Skill skill in _chemicalAlternatives)
            {
                skill.CheckUnlockSkill();
            }
            foreach (Skill skill in _extensiveEpidemiologicalStudies)
            {
                skill.CheckUnlockSkill();
            }
        }

    }
}
