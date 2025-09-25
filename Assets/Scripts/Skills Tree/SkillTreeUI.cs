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


    public List<GameObject> allObject;

    [Header("UI")]
    public GameObject root;
    public GameObject btnPrefab;
    public GameObject panelSkill;
    public TextMeshProUGUI panelTxtTitle, panelTxtDesc, btnPriceTxt, argentTxt;
    public Button btnBuy;

    private void Start()
    {
        currentTree = politicalTree;

        politicalTree.initialisation();

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
        panelSkill.SetActive(true);
        panelTxtTitle.text = title;
        panelTxtDesc.text = desc;
        btnPriceTxt.text = price + "€";

        if (GameManager.instance.money < price) btnBuy.interactable = false;
        else btnBuy.interactable = true;

            btnBuy.onClick.AddListener(() => { if (skill.BuySkill()) panelSkill.SetActive(false); setArgentText(); currentTree.SetUp(); });
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (panelSkill != null && panelSkill.activeSelf)
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(panelSkill.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera))
            {
                panelSkill.SetActive(false);
            }
        }
    }
}
