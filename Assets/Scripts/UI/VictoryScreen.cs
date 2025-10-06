using PFAS;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public TextMeshProUGUI dayText;

    private void OnEnable()
    {
        if (dayText != null) dayText.text = $"Il vous aura fallu {GameManager.instance.dayPass} jours";
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
    }
}
