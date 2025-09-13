using PFAS.Gameplay;
using UnityEngine;

public class SkillTreeUI : MonoBehaviour
{
    public PoliticalTree politicalTree;

    public GlobalTree currentTree;


    [Header("UI")]
    public GameObject root;
    public GameObject btnPrefab;

    private void Start()
    {
        currentTree = politicalTree;
    }

    private void OnEnable()
    {
        currentTree.root = root;
        currentTree.btnPrefab = btnPrefab;

        currentTree.SetUp();
    }
}
