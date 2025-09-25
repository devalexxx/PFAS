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

        protected List<GameObject> allObject;

        private void Start()
        {
            allObject = new List<GameObject>();
        }

        public virtual void SetUp()
        {
            allObject.ForEach(obj => { Destroy(obj); });
            allObject.Clear();
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

                    allObject.Add(obj);
                }
            }
        }
    }
}