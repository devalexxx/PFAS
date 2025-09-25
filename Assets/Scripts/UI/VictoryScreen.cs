using PFAS;
using TMPro;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    public TextMeshProUGUI dayText;

    private void OnEnable()
    {
        dayText.text = $"En {GameManager.instance.dayPass} jours";
    }
}
