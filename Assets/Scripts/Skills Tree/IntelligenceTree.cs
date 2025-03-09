using UnityEngine;

namespace PFAS.Gameplay
{
    public class IntelligenceTree : MonoBehaviour
    {
        private Skill[] _globalNetworkOfEnvironmentalSensors;
        private Skill[] _targetedAwarenessCampaigns;
        private Skill[] _industrialTracing;
        private Skill[] _scenarioSimulation;
        void Start()
        {
            _globalNetworkOfEnvironmentalSensors[0] = new Skill("Satellite deployment", "", 1, 0, 5, 10, -6, false);
            _globalNetworkOfEnvironmentalSensors[1] = new Skill("Autonomous terrestrial sensors", "", 2, 0, 4, 12, -7, false);
            _globalNetworkOfEnvironmentalSensors[2] = new Skill("Artificial intelligence for analysis", "", 3, 0, 6, 15, -8, false);

            _targetedAwarenessCampaigns[0] = new Skill("Public education", "", 5, 5, 0, 8, -4, false);
            _targetedAwarenessCampaigns[1] = new Skill("Industrial training", "", 6, 6, 0, 10, -5, false);
            _targetedAwarenessCampaigns[2] = new Skill("Citizen mobilisation", "", 7, 7, 0, 12, -6, false);

            _industrialTracing[0] = new Skill("Production monitoring system", "", 8, 8, 0, 10, -6, false);
            _industrialTracing[1] = new Skill("Reinforced audits", "", 5, 5, 0, 12, -7, false);
            _industrialTracing[2] = new Skill("Penalties for concealment", "", 8, 8, 0, 6, -5, false);

            _scenarioSimulation[0] = new Skill("Predictive contamination models", "", 1, 0, 5, 10, -6, false);
            _scenarioSimulation[1] = new Skill("Impact studies on flora and fauna", "", 2, 0, 4, 8, -4, false);
            _scenarioSimulation[2] = new Skill("Preparing for health crises", "", 3, 5, 0, 12, -7, false);
        }

    }
}