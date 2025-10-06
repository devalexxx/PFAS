using UnityEngine;
using UnityEngine.SceneManagement;

namespace PFAS.MainMenu
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private string gameSceneName;

        [SerializeField] private GameObject menuPanel;
        [SerializeField] private GameObject creditsPanel;

        public void PlayGame()
        {
            if (!string.IsNullOrEmpty(gameSceneName))
            {
                SceneManager.LoadScene(gameSceneName);
            }
            else
            {
                Debug.LogWarning("Nom de la scène non défini !");
            }
        }

        public void ShowCredits()
        {
            if (creditsPanel != null && menuPanel != null)
            {
                creditsPanel.SetActive(true);
                menuPanel.SetActive(false);
            }
        }
        public void HideCredits()
        {
            if (creditsPanel != null && menuPanel != null)
            {
                creditsPanel.SetActive(false);
                menuPanel.SetActive(true);
            }
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}