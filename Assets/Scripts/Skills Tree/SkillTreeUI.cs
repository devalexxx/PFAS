using MyBox;
using PFAS;
using PFAS.Gameplay;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillTreeUI : MonoBehaviour, IPointerClickHandler
{
    public PoliticalTree politicalTree;
    public TechnologicalTree technologicalTree;
    public IntelligenceTree intelligenceTree;

    public GlobalTree currentTree;

    public Skill selectedUpgrade { get; private set; } = null;

    public List<GameObject> allObject;

    [Header("UI")]
    public GameObject root;
    public GameObject btnPrefab;
    public GameObject panelSkill;
    public TextMeshProUGUI panelTxtTitle, panelTxtDesc, btnPriceTxt, argentTxt;
    public Button btnBuy;
    public Button[] btnsPanel;

    private void Start()
    {
        currentTree = politicalTree;

        politicalTree.initialisation();
        btnsPanel[0].interactable = false;

        gameObject.SetActive(false);
    }

    public void changeTree(int id)
    {
        switch (id)
        {
            case 0: currentTree = politicalTree; break;
            case 1: currentTree = technologicalTree; break;
            case 2: currentTree = intelligenceTree; break;
            default: break;
        }

        for (int i = 0; i < btnsPanel.Length; i++)
        {
            btnsPanel[i].interactable = i != id;
        }

        currentTree.root = root;
        currentTree.btnPrefab = btnPrefab;
        currentTree.SetUp();
    }

    public void setArgentText()
    {
        argentTxt.text = $"{GameManager.instance.money}€";
    }

    private void OnEnable()
    {
        currentTree.root = root;
        currentTree.btnPrefab = btnPrefab;

        currentTree.SetUp();

        setArgentText();
    }

    public void SelectSkill(string title, string desc, int price, Skill skill)
    {
        if(panelSkill.activeSelf && selectedUpgrade == skill)
        {
            panelSkill.SetActive(false);
            selectedUpgrade = null;
        }
        else 
        {
            panelSkill.SetActive(true);
            panelTxtTitle.text = title;
            panelTxtDesc.text = desc;
            btnPriceTxt.text = price + "€";

            if (GameManager.instance.money < price) btnBuy.interactable = false;
            else btnBuy.interactable = true;
            
            btnBuy.onClick.AddListener(() => { if (skill.BuySkill()) panelSkill.SetActive(false); setArgentText(); currentTree.SetUp(); });

            selectedUpgrade = skill;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (panelSkill == null || !panelSkill.activeSelf)
            return;

        if (!RectTransformUtility.RectangleContainsScreenPoint(
            panelSkill.GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera))
        {
            panelSkill.SetActive(false);
            selectedUpgrade = null;
        }
    }
}
