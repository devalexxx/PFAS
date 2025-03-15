using UnityEngine;

namespace PFAS.UI
{
    public class UIPausePanel : MonoBehaviour
    {
        public void Toggle()
        {
            gameObject.SetActive(!gameObject.activeSelf);
            Time.timeScale = gameObject.activeSelf ? 0f : 1f;
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
