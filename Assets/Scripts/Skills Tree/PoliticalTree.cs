using System.Collections.Generic;
using UnityEngine;

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

            _acceleratedLegislation[0] = new Skill("Regulatory emergency commitees", "", 1, 8, 0, 0, -5, false, new Vector2(-479, 192.97f));
            _acceleratedLegislation[1] = new Skill("Simplified procedures", "", 2, 10, 0, 0, -7, false, new Vector2(-279, 192.97f));
            _acceleratedLegislation[2] = new Skill("Progressive bans", "", 3, 12, 0, 0, -10, false, new Vector2(-79, 192.97f));

            _environmentalSubsidies[0] = new Skill("Tax incentives", "", 5, 5, 0, 0, -5, false);
            _environmentalSubsidies[1] = new Skill("Research grants", "", 8, 4, 8, 0, 0, false);
            _environmentalSubsidies[2] = new Skill("Eco-label", "", 3, 0, 0, 6, -3, false);

            _internationalStandards[0] = new Skill("Global treaties", "", 4, 12, 0, 0, -8, false);
            _internationalStandards[1] = new Skill("Harmonisation of thresholds", "", 5, 10, 0, 5, -6, false);
            _internationalStandards[2] = new Skill("Intergovernmental cooperation", "", 6, 8, 0, 4, -5, false);

            _pfasTaxes[0] = new Skill("Progressive taxation", "", 7, 10, 0, 0, -7, false);
            _pfasTaxes[1] = new Skill("Fines for pollution", "", 8, 6, 0, 0, -5, false);
            _pfasTaxes[2] = new Skill("Reinvestment of taxes", "", 9, 0, 8, 0, -6, false);
        }

        public override void SetUp()
        {
            initialisation();

            foreach (var item in _acceleratedLegislation)
            {
                if (item.pos != Vector2.zero)
                {
                    Debug.Log("Prefab: " + btnPrefab);
                    Debug.Log("Root: " + root);

                    var obj = Instantiate(btnPrefab, root.transform);

                    if (obj == null) Debug.LogError("obj est null !");

                    RectTransform rect = obj.GetComponent<RectTransform>();
                    if (rect == null) Debug.LogError("RectTransform est null sur " + obj.name);

                    rect.anchoredPosition = item.pos;

                    if (allObject == null) Debug.LogError("allObject est null !");

                    allObject.Add(obj);
                }
            }
        }

    }
}