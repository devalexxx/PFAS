using UnityEngine;

namespace PFAS.Gameplay
{
    public class PoliticalTree : GlobalTree
    {
        private Skill[] _acceleratedLegislation;
        private Skill[] _environmentalSubsidies;
        private Skill[] _internationalStandards;
        private Skill[] _pfasTaxes;

        private void Start()
        {
            _acceleratedLegislation[0] = new Skill("Regulatory emergency commitees", "", 1, 8, 0, 0, -5);
            _acceleratedLegislation[1] = new Skill("Simplified procedures", "", 2, 10, 0, 0, -7);
            _acceleratedLegislation[2] = new Skill("Progressive bans", "", 3, 12, 0, 0, -10);

            _environmentalSubsidies[0] = new Skill("Tax incentives", "", 5, 5, 0, 0, -5);
            _environmentalSubsidies[1] = new Skill("Research grants", "", 8, 4, 8, 0, 0);
            _environmentalSubsidies[2] = new Skill("Eco-label", "", 3, 0, 0, 6, -3);

            _internationalStandards[0] = new Skill("Global treaties", "", 4, 12, 0, 0, -8);
            _internationalStandards[1] = new Skill("Harmonisation of thresholds", "", 5, 10, 0, 5, -6);
            _internationalStandards[2] = new Skill("Intergovernmental cooperation", "", 6, 8, 0, 4, -5);

            _pfasTaxes[0] = new Skill("Progressive taxation", "", 7, 10, 0, 0, -7);
            _pfasTaxes[1] = new Skill("Fines for pollution", "", 8, 6, 0, 0, -5);
            _pfasTaxes[2] = new Skill("Reinvestment of taxes", "", 9, 0, 8, 0, -6);
        }
    }
}