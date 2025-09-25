using System.Collections.Generic;
using UnityEngine;

namespace PFAS.Gameplay
{
    public class TechnologicalTree : GlobalTree
    {
        private Skill[] _advancedDetectionMethods = new Skill[3];
        private Skill[] _innovativeDecontaminationTechnologies = new Skill[3];
        private Skill[] _chemicalAlternatives = new Skill[3];
        private Skill[] _extensiveEpidemiologicalStudies = new Skill[3];

        private void Start()
        {
            initialisation();
        }

        private void initialisation()
        {
            ui.allObject = new List<GameObject>();

            _advancedDetectionMethods[0] = new Skill("Environmental nanosensors", "", 1, 0, 10, 6, -5, false, new Vector2(-479, 192.97f));
            _advancedDetectionMethods[1] = new Skill("Rapid tests", "", 2, 0, 8, 8, -4, false, new Vector2(-279, 192.97f));
            _advancedDetectionMethods[2] = new Skill("Genetic analysis of PFAS", "", 3, 5, 12, 0, -6, false, new Vector2(-79, 192.97f));

            SetUpPreviousAfter(_advancedDetectionMethods);

            _innovativeDecontaminationTechnologies[0] = new Skill("Bioremediation", "", 5, 0, 15, 0, -12, false, new Vector2(-479, -8.97f));
            _innovativeDecontaminationTechnologies[1] = new Skill("Advanced adsorption techniques", "", 8, 0, 10, 0, -8, false, new Vector2(-279, -8.97f));
            _innovativeDecontaminationTechnologies[2] = new Skill("Chemical destruction processes", "", 3, 0, 12, 0, -10, false, new Vector2(-79, -8.97f));

            SetUpPreviousAfter(_innovativeDecontaminationTechnologies);

            _chemicalAlternatives[0] = new Skill("Hydrophobic materials without PFAS", "", 4, 5, 8, 0, -6, false, new Vector2(-479, -208.97f));
            _chemicalAlternatives[1] = new Skill("Biodegradable substitutes", "", 5, 5, 10, 0, -7, false, new Vector2(-279, -208.97f));
            _chemicalAlternatives[2] = new Skill("Changes to production chains", "", 6, 6, 12, 0, -8, false, new Vector2(-79, -208.97f));

            SetUpPreviousAfter(_chemicalAlternatives);

            _extensiveEpidemiologicalStudies[0] = new Skill("Mapping of PFAS-related diseases", "", 7, 0, 8, 8, 5, false, new Vector2(-479, -408.97f));
            _extensiveEpidemiologicalStudies[1] = new Skill("Advanced bioassays", "", 8, 0, 10, 10, 0, false, new Vector2(-279, -408.97f));
            _extensiveEpidemiologicalStudies[2] = new Skill("Impact on reproduction and development", "", 9, 6, 8, 5, 0, false, new Vector2(-79, -408.97f));
            
            SetUpPreviousAfter(_extensiveEpidemiologicalStudies);
        }

        public override void SetUp()
        {
            base.SetUp();

            ShowUpgrades(_advancedDetectionMethods);
            ShowUpgrades(_innovativeDecontaminationTechnologies);
            ShowUpgrades(_chemicalAlternatives);
            ShowUpgrades(_extensiveEpidemiologicalStudies);
        }
    }
}
