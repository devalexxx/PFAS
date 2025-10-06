using UnityEngine;
namespace PFAS.UI
{
    public class UIPausePanel : MonoBehaviour
    {
        [SerializeField] private TutoManager _tutoManager;

        public void Toggle()
        {
            gameObject.SetActive(!gameObject.activeSelf);
            Time.timeScale = gameObject.activeSelf ? 0f : 1f;
        }

        public void Tuto()
        {
            _tutoManager.StartTuto();
            Toggle();
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}