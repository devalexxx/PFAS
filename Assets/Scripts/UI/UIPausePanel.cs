using UnityEngine;

public class UIPausePanel : MonoBehaviour
{
    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        Time.timeScale = gameObject.activeSelf ? 1f : 0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
