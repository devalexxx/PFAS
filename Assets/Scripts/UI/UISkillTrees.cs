using PFAS.Gameplay;
using UnityEngine;
using UnityEngine.UI;

public class UISkillTrees : MonoBehaviour
{

    public GameObject TechnologicalTree;
    public GameObject IntelligenceTree;
    public GameObject PoliticalTree;

    public void Start()
    {
        TechnologicalTree.SetActive(false);
        IntelligenceTree.SetActive(false);
        PoliticalTree.SetActive(false);
    }
    public void DisplayTechnologicalTree()
    {
        TechnologicalTree.transform.Find("Tree").gameObject.SetActive(true);
        IntelligenceTree.transform.Find("Tree").gameObject.SetActive(false);
        PoliticalTree.transform.Find("Tree").gameObject.SetActive(false);
    }
    public void DisplayIntelligenceTree()
    {
        TechnologicalTree.transform.Find("Tree").gameObject.SetActive(false);
        IntelligenceTree.transform.Find("Tree").gameObject.SetActive(true);
        PoliticalTree.transform.Find("Tree").gameObject.SetActive(false);
    }
    public void DisplayPoliticalTree()
    {
        TechnologicalTree.transform.Find("Tree").gameObject.SetActive(false);
        IntelligenceTree.transform.Find("Tree").gameObject.SetActive(false);
        PoliticalTree.transform.Find("Tree").gameObject.SetActive(true);
    }

    public void DisplaySkillTree()
    {
        TechnologicalTree.SetActive(true);
        IntelligenceTree.SetActive(true);
        IntelligenceTree.transform.Find("Tree").gameObject.SetActive(false);
        PoliticalTree.SetActive(true);
        PoliticalTree.transform.Find("Tree").gameObject.SetActive(false);
    }

    public void BuySkill(Button clickedButton)
    {
        clickedButton.interactable = !clickedButton.GetComponent<Skill>().BuySkill();
    }
}
