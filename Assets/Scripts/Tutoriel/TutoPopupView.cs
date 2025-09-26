using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutoPopupView : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;   // ou Text si tu n'utilises pas TMP
    [SerializeField] private TMP_Text bodyText;    // ou Text
    [SerializeField] private Button confirmButton; // “Compris !”

    public void Show(string title, string body, UnityAction onConfirm, string buttonLabel = "Compris !")
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

        gameObject.SetActive(true);
    }

    public void Hide() => gameObject.SetActive(false);
}
