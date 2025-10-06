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

            _advancedDetectionMethods[0] = new Skill("Nanocapteurs environnementaux",
                "De minuscules espions invisibles se faufilent partout : air, eau, sol… Les PFAS n’ont plus nulle part où se cacher. Big Brother devient chimique." +
                "\r\nEffet : améliore la surveillance globale de la pollution." +
                "\r\n+10% Technologie, +6% Prévention, -5% Pollution globale",
                1, 0, 10, 6, -5, false, new Vector2(-479, 192.97f));
            _advancedDetectionMethods[1] = new Skill("Tests rapides",
                "Fini les délais interminables en laboratoire. Avec ces kits portables, même un stagiaire peut repérer une contamination en quelques minutes." +
                "\r\nEffet : permet d’agir immédiatement sur les zones polluées." +
                "\r\n+8% Technologie, +8% Prévention, -4% Pollution globale",
                2, 0, 8, 8, -4, false, new Vector2(-279, 192.97f));
            _advancedDetectionMethods[2] = new Skill("Analyse des PFAS",
                "Chaque PFAS est traqué comme un criminel avec son ADN chimique. On identifie les pires coupables et leurs origines. CSI : Pollution Edition" +
                "\r\nEffet : priorise l’élimination des variantes les plus toxiques." +
                "\r\n+12% Technologie, +5% Régulation, -6% Pollution globale",
                3, 5, 12, 0, -6, false, new Vector2(-79, 192.97f));

            SetUpPreviousAfter(_advancedDetectionMethods);

            _innovativeDecontaminationTechnologies[0] = new Skill("Biorémédiation",
                "Des bactéries transforment les PFAS en nutriments. L’écosystème devient son propre nettoyeur… tant que personne ne décide de breveter les bactéries." +
                "\r\nEffet : réduit naturellement la pollution sur le long terme." +
                "\r\n+15% Technologie, -12% Pollution globale",
                5, 0, 15, 0, -12, false, new Vector2(-479, -8.97f));
            _innovativeDecontaminationTechnologies[1] = new Skill("Techniques d’adsorption avancées",
                "De nouveaux filtres surpuissants capturent les PFAS comme des moustiquaires attrapent les moustiques. Sauf que cette fois, personne ne réchappe." +
                "\r\nEffet : améliore le traitement de l’eau et de l’air contaminés." +
                "\r\n+10% Technologie, -8% Pollution globale",
                8, 0, 10, 0, -8, false, new Vector2(-279, -8.97f));
            _innovativeDecontaminationTechnologies[2] = new Skill("Procédés de destruction chimique",
                "Si tu ne peux pas les filtrer, brûle-les. Des réactions ciblées brisent les PFAS en molécules inoffensives. La chimie reprend ses droits… contre elle-même." +
                "\r\nEffet : élimine directement les PFAS existants." +
                "\r\n+12% Technologie, -10% Pollution globale",
                3, 0, 12, 0, -10, false, new Vector2(-79, -8.97f));

            SetUpPreviousAfter(_innovativeDecontaminationTechnologies);

            _chemicalAlternatives[0] = new Skill("Matériaux hydrophobes sans PFAS",
                "Toujours aussi résistants à l’eau et aux graisses… mais sans tuer l’environnement. Qui a dit que la chimie ne pouvait pas être sympa ?" +
                "\r\nEffet : réduit la dépendance industrielle aux PFAS." +
                "\r\n+8% Technologie, +5% Régulation, -6% Pollution globale",
                4, 5, 8, 0, -6, false, new Vector2(-479, -208.97f));
            _chemicalAlternatives[1] = new Skill("Substituts biodégradables",
                "Des molécules écologiques prennent la relève. Enfin une innovation qui ne se décompose pas… sauf quand il le faut." +
                "\r\nEffet : accélère la transition vers des produits PFAS-free." +
                "\r\n+10% Technologie, +5% Régulation, -7% Pollution globale",
                5, 5, 10, 0, -7, false, new Vector2(-279, -208.97f));
            _chemicalAlternatives[2] = new Skill("Modifications des chaînes de production",
                "Les usines s’adaptent. Moins de PFAS, plus d’efficacité. Et comme toujours : ‘c’est pour des raisons écologiques’ (et aussi parce que c’est rentable)." +
                "\r\nEffet : généralise l’abandon des PFAS dans l’industrie." +
                "\r\n+12% Technologie, +6% Régulation, -8% Pollution globale",
                6, 6, 12, 0, -8, false, new Vector2(-79, -208.97f));

            SetUpPreviousAfter(_chemicalAlternatives);

            _extensiveEpidemiologicalStudies[0] = new Skill("Cartographie des maladies liées aux PFAS",
                "Chaque cancer, chaque pathologie est relié à une carte géante. Le puzzle morbide se complète peu à peu… et il n’est pas joli à voir." +
                "\r\nEffet : augmente la pression publique pour agir contre les PFAS." +
                "\r\n+8% Technologie, +8% Prévention, +5% Régulation",
                7, 0, 8, 8, 5, false, new Vector2(-479, -408.97f));
            _extensiveEpidemiologicalStudies[1] = new Skill("Tests biologiques avancés",
                "Quelques gouttes de sang suffisent pour savoir combien de PFAS circulent dans ton corps. Spoiler : probablement trop." +
                "\r\nEffet : améliore la détection de l’exposition humaine." +
                "\r\n+10% Technologie, +10% Prévention",
                8, 0, 10, 10, 0, false, new Vector2(-279, -408.97f));
            _extensiveEpidemiologicalStudies[2] = new Skill("Impact sur la reproduction et le développement",
                "Les études montrent que les PFAS s’attaquent même aux générations futures. Rien de tel que menacer les bébés pour faire réagir les politiques." +
                "\r\nEffet : alimente les campagnes de sensibilisation et accélère l’adoption de mesures strictes." +
                "\r\n+8% Technologie, +6% Régulation, +5% Prévention",
                9, 6, 8, 5, 0, false, new Vector2(-79, -408.97f));
            
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
