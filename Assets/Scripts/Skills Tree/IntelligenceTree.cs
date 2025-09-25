using System.Collections.Generic;
using UnityEngine;

namespace PFAS.Gameplay
{
    public class IntelligenceTree : GlobalTree
    {
        private Skill[] _globalNetworkOfEnvironmentalSensors = new Skill[3];
        private Skill[] _targetedAwarenessCampaigns = new Skill[3];
        private Skill[] _industrialTracing = new Skill[3];
        private Skill[] _scenarioSimulation = new Skill[3];

        private void Start()
        {
            initialisation();
        }

        void initialisation()
        {
            ui.allObject = new List<GameObject>();

            _globalNetworkOfEnvironmentalSensors[0] = new Skill("Satellite deployment", "", 1, 0, 5, 10, -6, false, new Vector2(-479, 192.97f));
            _globalNetworkOfEnvironmentalSensors[1] = new Skill("Autonomous terrestrial sensors", "", 2, 0, 4, 12, -7, false, new Vector2(-279, 192.97f));
            _globalNetworkOfEnvironmentalSensors[2] = new Skill("Artificial intelligence for analysis", "", 3, 0, 6, 15, -8, false, new Vector2(-79, 192.97f));

            SetUpPreviousAfter(_globalNetworkOfEnvironmentalSensors);

            _targetedAwarenessCampaigns[0] = new Skill("Public education", "", 5, 5, 0, 8, -4, false, new Vector2(-479, -8.97f));
            _targetedAwarenessCampaigns[1] = new Skill("Industrial training", "", 6, 6, 0, 10, -5, false, new Vector2(-279, -8.97f));
            _targetedAwarenessCampaigns[2] = new Skill("Citizen mobilisation", "", 7, 7, 0, 12, -6, false, new Vector2(-79, -8.97f));

            SetUpPreviousAfter(_targetedAwarenessCampaigns);

            _industrialTracing[0] = new Skill("Production monitoring system", "", 8, 8, 0, 10, -6, false, new Vector2(-479, -208.97f));
            _industrialTracing[1] = new Skill("Reinforced audits", "", 5, 5, 0, 12, -7, false, new Vector2(-279, -208.97f));
            _industrialTracing[2] = new Skill("Penalties for concealment", "", 8, 8, 0, 6, -5, false, new Vector2(-79, -208.97f));

            SetUpPreviousAfter(_industrialTracing);

            _scenarioSimulation[0] = new Skill("Predictive contamination models", "", 1, 0, 5, 10, -6, false, new Vector2(-479, -408.97f));
            _scenarioSimulation[1] = new Skill("Impact studies on flora and fauna", "", 2, 0, 4, 8, -4, false, new Vector2(-279, -408.97f));
            _scenarioSimulation[2] = new Skill("Preparing for health crises", "", 3, 5, 0, 12, -7, false, new Vector2(-79, -408.97f));

            SetUpPreviousAfter(_scenarioSimulation);
        }

        public override void SetUp()
        {
            base.SetUp();

            ShowUpgrades(_globalNetworkOfEnvironmentalSensors);
            ShowUpgrades(_targetedAwarenessCampaigns);
            ShowUpgrades(_industrialTracing);
            ShowUpgrades(_scenarioSimulation);
        }
    }
}