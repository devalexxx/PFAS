using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutoPopupView : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text bodyText;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button SkipButton;

    public void Show(string title, string body, UnityAction onConfirm,UnityAction onSkip = null, string buttonLabel = "Compris !")
    {
        if (titleText) titleText.text = title;
        if (bodyText) bodyText.text = body;

        if (confirmButton)
        {
            var label = confirmButton.GetComponentInChildren<TMP_Text>();
            if (label) label.text = buttonLabel;

            confirmButton.onClick.RemoveAllListeners();
            confirmButton.onClick.AddListener(onConfirm);
        }

        if (SkipButton)
        {
            SkipButton.onClick.RemoveAllListeners();
            if (onSkip != null)
                SkipButton.onClick.AddListener(onSkip);
        }

        gameObject.SetActive(true);
    }

    public void Hide() => gameObject.SetActive(false);
}
