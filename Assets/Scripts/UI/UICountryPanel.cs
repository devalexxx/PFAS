using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PFAS.UI {
    public class UICountryPanel : MonoBehaviour
    {
        private TextMeshProUGUI _countryNameText;
        //private Text _countryInfoText;



        private void Awake()
        {
            // Récupère les références aux éléments UI
            _countryNameText = transform.Find("CountryName").GetComponent<TextMeshProUGUI>();
            //_countryInfoText = transform.Find("CountryInfo").GetComponent<Text>();
        }

        // Affiche le panneau
        public void Show(GameObject p_country)
        {
            gameObject.SetActive(true);
            SetCountry(p_country);
        }

        // Cache le panneau
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void SetCountry(GameObject country)
        {
            // Récupère les infos du pays (via un script attaché au pays ou autre) et met à jour l'UI
            _countryNameText.text = country.name;
        }
    }
}