using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PFAS.Gameplay
{
    public class PoliticalTree : GlobalTree
    {
        [SerializeField]
        private Skill[] _acceleratedLegislation = new Skill[3];
        private Skill[] _environmentalSubsidies = new Skill[3];
        private Skill[] _internationalStandards = new Skill[3];
        private Skill[] _pfasTaxes = new Skill[3];

        private void Start()
        {
            initialisation();
        }

        public void initialisation()
        {
            allObject = new List<GameObject>();

            _acceleratedLegislation[0] = new Skill("Regulatory emergency commitees", 
                "Création de comités d’urgence réglementaires chargés d’accélérer la prise de décision en cas de crise sanitaire ou environnementale." +
                "\r\nCes comités réduisent les délais administratifs et permettent de mettre rapidement en place des mesures restrictives, mais la précipitation peut " +
                "entraîner une perte de contrôle et de concertation.", 
                1, 8, 0, 0, -5, false, new Vector2(-479, 192.97f), new Skill[] { _acceleratedLegislation[0], _acceleratedLegislation[1] }, new Skill[] { });


            _acceleratedLegislation[1] = new Skill("Simplified procedures", "", 2, 10, 0, 0, -7, false, new Vector2(-279, 192.97f), new Skill[] { _acceleratedLegislation[1] }, new Skill[] { _acceleratedLegislation[0] });
            _acceleratedLegislation[2] = new Skill("Progressive bans", "", 10, 12, 0, 0, -10, false, new Vector2(-79, 192.97f), new Skill[] { }, new Skill[] { _acceleratedLegislation[0], _acceleratedLegislation[1] });

            _environmentalSubsidies[0] = new Skill("Tax incentives", "", 5, 5, 0, 0, -5, false, Vector2.zero);
            _environmentalSubsidies[1] = new Skill("Research grants", "", 8, 4, 8, 0, 0, false, Vector2.zero);
            _environmentalSubsidies[2] = new Skill("Eco-label", "", 3, 0, 0, 6, -3, false, Vector2.zero);

            _internationalStandards[0] = new Skill("Global treaties", "", 4, 12, 0, 0, -8, false, Vector2.zero);
            _internationalStandards[1] = new Skill("Harmonisation of thresholds", "", 5, 10, 0, 5, -6, false, Vector2.zero);
            _internationalStandards[2] = new Skill("Intergovernmental cooperation", "", 6, 8, 0, 4, -5, false, Vector2.zero);

            _pfasTaxes[0] = new Skill("Progressive taxation", "", 7, 10, 0, 0, -7, false, Vector2.zero);
            _pfasTaxes[1] = new Skill("Fines for pollution", "", 8, 6, 0, 0, -5, false, Vector2.zero);
            _pfasTaxes[2] = new Skill("Reinvestment of taxes", "", 9, 0, 8, 0, -6, false, Vector2.zero);
        }

        public override void SetUp()
        {
            ShowUpgrades(_acceleratedLegislation);
        }

    }
}