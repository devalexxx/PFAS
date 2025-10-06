using System.Collections.Generic;
using TMPro;
using UnityEditor;
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
            ui.allObject = new List<GameObject>();

            _acceleratedLegislation[0] = new Skill("Comités d'urgence réglementaire",
                "Quand les bureaucrates s’ennuient, ils forment un comité. Cette fois, ça tombe bien : des experts sont réunis pour couper court aux débats interminables et recommander des lois rapides. La science devient enfin plus rapide que la politique." +
                "\r\nEffet : accélère la vitesse d’adoption des lois anti-PFAS." +
                "\r\n+8% Régulation, -5% Pollution globale", 
                20, 8, 0, 0, -5, false, new Vector2(-479, 192.97f));
            _acceleratedLegislation[1] = new Skill("Procédures simplifiées",
                "Les formulaires disparaissent mystérieusement, les délais s’évaporent, et les fonctionnaires se mettent à tamponner plus vite que leur ombre. Résultat : les restrictions tombent avant que les industriels n’aient eu le temps de réagir." +
                "\r\nEffet : réduit fortement le délai entre proposition et application des lois." +
                "\r\n+10% Régulation, -7% Pollution globale",
                2, 10, 0, 0, -7, false, new Vector2(-279, 192.97f));
            _acceleratedLegislation[2] = new Skill("Interdictions progressives", 
                "Plutôt que d’affoler tout le monde d’un coup, les gouvernements adoptent une stratégie douce mais implacable : on commence par les usages les plus inutiles, puis on resserre l’étau petit à petit." +
                "\r\nEffet : élimine progressivement les PFAS tout en évitant une révolte économique." +
                "\r\n+12% Régulation, -10% Pollution globale",
                10, 12, 0, 0, -10, false, new Vector2(-79, 192.97f));

            SetUpPreviousAfter(_acceleratedLegislation);

            _environmentalSubsidies[0] = new Skill("Incitations fiscales",
                "Rien ne motive mieux qu’un bon rabais sur les impôts. Les entreprises découvrent soudain que sauver la planète peut aussi remplir leurs poches. Miracle de la fiscalité verte !" +
                "\r\nEffet : accélère l’innovation privée et la transition vers des alternatives aux PFAS." +
                "\r\n+5% Régulation, +5% Technologie, -5% Pollution globale",
                5, 5, 0, 0, -5, false, new Vector2(-479, -8.97f));
            _environmentalSubsidies[1] = new Skill("Aides à la recherche",
                "Les labos reçoivent un joli chèque pour chercher des solutions miracles. Grâce au financement public, la science se met en mode turbo… et parfois, elle trouve vraiment quelque chose !" +
                "\r\nEffet : augmente la vitesse de découverte de substituts aux PFAS." +
                "\r\n+8% Technologie, +4% Régulation",
                8, 4, 8, 0, 0, false, new Vector2(-279, -8.97f));
            _environmentalSubsidies[2] = new Skill("Label écologique",
                "Un autocollant vert magique apparaît sur les produits : ‘Sans PFAS’. Les consommateurs se sentent soudain vertueux à chaque achat… et les entreprises comprennent vite qu’il vaut mieux l’avoir que de passer pour des pollueurs." +
                "\r\nEffet : booste la demande pour les produits alternatifs, accélérant la disparition des PFAS du marché." +
                "\r\n+6% Prévention, -3% Pollution globale",
                3, 0, 0, 6, -3, false, new Vector2(-79, -8.97f));

            SetUpPreviousAfter(_environmentalSubsidies);

            _internationalStandards[0] = new Skill("Traités mondiaux",
                "Les gouvernements se serrent la main, signent des papiers et se félicitent devant les caméras. Derrière les sourires diplomatiques, un vrai changement : limiter la production et l’exportation de PFAS devient une promesse internationale… au moins sur le papier." +
                "\r\nEffet : réduit la dissémination des PFAS à l’échelle globale." +
                "\r\n+12% Régulation, -8% Pollution globale",
                4, 12, 0, 0, -8, false, new Vector2(-479, -208.97f));
            _internationalStandards[1] = new Skill("Harmonisation des seuils",
                "Chaque pays avait sa propre limite, souvent arbitraire. Mais quand tout le monde adopte les mêmes chiffres, la comparaison devient impossible à esquiver. Résultat : les industries ne peuvent plus jouer à cache-cache avec les réglementations." +
                "\r\nEffet : uniformise la lutte mondiale, rendant les interdictions plus efficaces." +
                "\r\n+10% Régulation, +5% Prévention, -6% Pollution globale",
                5, 10, 0, 5, -6, false, new Vector2(-279, -208.97f));
            _internationalStandards[2] = new Skill("Collaboration intergouvernementale",
                "Au lieu de travailler chacun dans leur coin, les États décident de partager leurs données. Un peu comme un gigantesque groupe WhatsApp des agences sanitaires, mais avec moins de memes et plus de graphiques inquiétants." +
                "\r\nEffet : améliore la recherche collective et accélère les solutions globales." +
                "\r\n+8% Régulation, +4% Prévention, -5% Pollution globale",
                6, 8, 0, 4, -5, false, new Vector2(-79, -208.97f));

            SetUpPreviousAfter(_internationalStandards);

            _pfasTaxes[0] = new Skill("Taxation progressive",
                "Un nouvel impôt s’abat sur les entreprises qui persistent à utiliser des PFAS. Résultat : soudain, les alternatives ‘trop chères’ deviennent incroyablement abordables. Comme quoi, la peur du fisc est plus forte que l’amour des produits chimiques." +
                "\r\nEffet : réduit l’utilisation industrielle de PFAS grâce à la pression économique." +
                "\r\n+10% Régulation, -7% Pollution globale",
                7, 10, 0, 0, -7,false, new Vector2(-479, -408.97f));
            _pfasTaxes[1] = new Skill("Amendes pour pollution",
                "Attrapés la main dans le pot de PFAS ? Les industriels pollueurs écopent de lourdes sanctions financières. Plus ils persistent, plus ça coûte cher… au point que certains préfèrent enfin respecter la loi plutôt que de payer l’addition." +
                "\r\nEffet : dissuade fortement les rejets illégaux de PFAS." +
                "\r\n+6% Régulation, -5% Pollution globale",
                8, 6, 0, 0, -5, false, new Vector2(-279, -408.97f));
            _pfasTaxes[2] = new Skill("Réinvestissement des taxes",
                "L’argent récolté ne disparaît pas dans les méandres des budgets publics : il est recyclé dans la dépollution et la recherche. Ironie du sort : les pollueurs financent malgré eux leur propre fin." +
                "\r\nEffet : augmente les fonds pour accélérer la recherche et nettoyer les sites contaminés." +
                "\r\n+8% Technologie, -6% Pollution globale",
                9, 0, 8, 0, -6, false, new Vector2(-79, -408.97f));

            SetUpPreviousAfter(_pfasTaxes);
        }

        public override void SetUp()
        {
            base.SetUp();

            ShowUpgrades(_acceleratedLegislation);
            ShowUpgrades(_environmentalSubsidies);
            ShowUpgrades(_internationalStandards);
            ShowUpgrades(_pfasTaxes);
        }

    }
}