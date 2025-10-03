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

            _globalNetworkOfEnvironmentalSensors[0] = new Skill("Déploiement satellitaire",
                "L’espace devient ton meilleur allié : des satellites scrutent la Terre, traquant chaque trace de PFAS. Même l’industrie la plus éloignée ne peut plus se cacher dans l’ombre" +
                "\r\nEffet : permet une surveillance globale et rapide de la contamination." +
                "\r\n+10% Prévention, +5% Technologie, -6% Pollution globale",
                1, 0, 5, 10, -6, false, new Vector2(-479, 192.97f));
            _globalNetworkOfEnvironmentalSensors[1] = new Skill("Capteurs autonomes terrestres",
                "Des stations automatiques plantées dans le sol et l’eau enregistrent tout en silence. Impossible pour les PFAS de se glisser incognito dans les nappes phréatiques." +
                "\r\nEffet : améliore la détection locale, surtout dans les zones à risque." +
                "\r\n+12% Prévention, +4% Technologie, -7% Pollution globale",
                2, 0, 4, 12, -7, false, new Vector2(-279, 192.97f));
            _globalNetworkOfEnvironmentalSensors[2] = new Skill("Intelligence artificielle pour l'analyse",
                "Des algorithmes croquent les données en temps réel. Résultat : la carte de la pollution se dessine avant même que les PFAS aient fini de se répandre." +
                "\r\nEffet : accélère la réactivité face aux contaminations." +
                "\r\n+15% Prévention, +6% Technologie, -8% Pollution globale",
                3, 0, 6, 15, -8, false, new Vector2(-79, 192.97f));

            SetUpPreviousAfter(_globalNetworkOfEnvironmentalSensors);

            _targetedAwarenessCampaigns[0] = new Skill("Éducation grand public",
                "Des cours à l’école, des pubs à la télé : les PFAS deviennent le nouveau grand méchant. Le message passe : ‘Si c’est éternel, c’est pas normal’." +
                "\r\nEffet : augmente la pression sociale pour des interdictions rapides." +
                "\r\n+8% Prévention, +5% Régulation, -4% Pollution globale",
                5, 5, 0, 8, -4, false, new Vector2(-479, -8.97f));
            _targetedAwarenessCampaigns[1] = new Skill("Formation des industriels",
                "Séminaires, powerpoints et certificats verts : les entreprises apprennent à se passer de PFAS… ou au moins à prétendre qu’elles essaient." +
                "\r\nEffet : améliore les pratiques industrielles." +
                "\r\n+10% Prévention, +6% Régulation, -5% Pollution globale",
                6, 6, 0, 10, -5, false, new Vector2(-279, -8.97f));
            _targetedAwarenessCampaigns[2] = new Skill("Mobilisation citoyenne",
                "Pancartes, pétitions et manifestations. Quand les citoyens crient assez fort, même les politiciens font semblant d’écouter… et parfois, ils agissent." +
                "\r\nEffet : accélère l’adoption de lois grâce à la pression populaire." +
                "\r\n+12% Prévention, +7% Régulation, -6% Pollution globale",
                7, 7, 0, 12, -6, false, new Vector2(-79, -8.97f));

            SetUpPreviousAfter(_targetedAwarenessCampaigns);

            _industrialTracing[0] = new Skill("Système de suivi des productions",
                "Chaque produit contenant des PFAS est marqué d’un code numérique. Les entreprises se retrouvent suivies à la trace, comme des criminels sous bracelet électronique." +
                "\r\nEffet : améliore la transparence des filières industrielles." +
                "\r\n+10% Prévention, +8% Régulation, -6% Pollution globale",
                8, 8, 0, 10, -6, false, new Vector2(-479, -208.97f));
            _industrialTracing[1] = new Skill("Audits renforcés",
                "Les inspecteurs débarquent plus souvent, plus nombreux et plus équipés. L’industrie n’a jamais aimé les visites surprises… surtout quand elles coûtent cher." +
                "\r\nEffet : réduit fortement les fraudes et rejets illégaux." +
                "\r\n+12% Prévention, +5% Régulation, -7% Pollution globale",
                5, 5, 0, 12, -7, false, new Vector2(-279, -208.97f));
            _industrialTracing[2] = new Skill("Sanctions en cas de dissimulation",
                "Tenter de cacher des PFAS devient un jeu dangereux : amendes record, interdictions d’activité, procès médiatisés. L’exemple est vite compris par les autres." +
                "\r\nEffet : dissuade les comportements frauduleux." +
                "\r\n+8% Régulation, +6% Prévention, -5% Pollution globale",
                8, 8, 0, 6, -5, false, new Vector2(-79, -208.97f));

            SetUpPreviousAfter(_industrialTracing);

            _scenarioSimulation[0] = new Skill("Modèles prédictifs de contamination",
                "Grâce aux supercalculateurs, on peut prévoir où les PFAS vont se propager… comme une météo toxique en temps réel." +
                "\r\nEffet : permet d’agir préventivement avant que la pollution ne s’aggrave." +
                "\r\n+10% Prévention, +5% Technologie, -6% Pollution globale",
                1, 0, 5, 10, -6, false, new Vector2(-479, -408.97f));
            _scenarioSimulation[1] = new Skill("Études d’impact sur la faune et la flore",
                "Les chercheurs simulent l’avenir des écosystèmes. Spoiler : sans action, c’est moche pour les poissons, les oiseaux… et les humains aussi." +
                "\r\nEffet : fournit des arguments solides pour justifier les restrictions." +
                "\r\n+8% Prévention, +4% Technologie, -4% Pollution globale",
                2, 0, 4, 8, -4, false, new Vector2(-279, -408.97f));
            _scenarioSimulation[2] = new Skill("Préparation aux crises sanitaires",
                "Quand une contamination massive frappe, des plans d’urgence s’activent immédiatement : distribution d’eau filtrée, évacuation ciblée… le chaos devient un peu plus gérable." +
                "\r\nEffet : réduit les conséquences sanitaires des accidents PFAS." +
                "\r\n+12% Prévention, +5% Régulation, -7% Pollution globale",
                3, 5, 0, 12, -7, false, new Vector2(-79, -408.97f));

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