using UnityEngine;
using UnityEngine.InputSystem;

namespace PFAS.UI {
    public class UIManager : MonoBehaviour
    {
        [Header("Pause Panel")]
        [SerializeField] private InputActionReference _pauseInput;
        [SerializeField] private UIPausePanel         _pausePanel;

        [Header("Country Panel")]
        [SerializeField] private UICountryPanel _countryPanel;

        void Awake()
        {
            _pauseInput.action.performed += _ => _pausePanel.Toggle();
        }

        void OnEnable()
        {
            _pauseInput.action.Enable();
        }

        void OnDisable()
        {
            _pauseInput.action.Disable();
        }

        public void ShowCountryPanel(GameObject p_selectedCountry)
        {
            _countryPanel.Show(p_selectedCountry);
        }

        public void HideCountryPanel()
        {
            _countryPanel.Hide();
        }
    }
}