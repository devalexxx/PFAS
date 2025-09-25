using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PFAS.Gameplay
{
    public class GlobalTree : MonoBehaviour
    {

        [HideInInspector]
        public GameObject root, btnPrefab;

        public SkillTreeUI ui;

        private void Start()
        {
            ui.allObject = new List<GameObject>();
        }

        public virtual void SetUp()
        {
            ui.allObject.ForEach(obj => { Destroy(obj); });
            ui.allObject.Clear();
        }

        public void ShowUpgrades(Skill[] skills)
        {
            foreach (var item in skills)
            {
                item.CheckUnlockSkill();

                if (item.pos != Vector2.zero && item.unlocked)
                {
                    var obj = Instantiate(btnPrefab, root.transform);

                    RectTransform rect = obj.GetComponent<RectTransform>();

                    rect.anchoredPosition = item.pos;

                    obj.GetComponentInChildren<TextMeshProUGUI>().text = item.name;

                    Button button = obj.GetComponent<Button>();

                    button.onClick.AddListener(() => { ui.SelectSkill(item.name, item.description, item.cost, item); });
                    button.interactable = true;

                    if (item.purchased) button.interactable = false;

                    ui.allObject.Add(obj);
                }
            }
        }

        public void SetUpPreviousAfter(Skill[] skills)
        {
            skills[0].after = new Skill[] { skills[1], skills[2] };
            skills[1].after = new Skill[] { skills[2] };
            skills[1].previous = new Skill[] { skills[0] };
            skills[2].previous = new Skill[] { skills[1], skills[0] };
        }
    }
}