using System.Linq;
using PFAS.Campagnes;
using PFAS.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PFAS.UI {
    public class UICountryPanel : MonoBehaviour
    {
        private Country _currentCountry;
        private City _currentCityDisplayed; // (null = Display country stats)

        [Header("Top Row")]
        [SerializeField] private TextMeshProUGUI _countryNameText;
        [SerializeField] private Transform _cityTabsContainer;
        [SerializeField] private Button _cityButtonPrefab;

        [Header("Stats")]
        [SerializeField] private TextMeshProUGUI _statVulnerabilityText;
        [SerializeField] private TextMeshProUGUI _statSocialResilienceText;
        [SerializeField] private TextMeshProUGUI _statAdaptabilityText;

        [Header("PFAS Progress")]
        [SerializeField] private RectTransform _pfasProgressBarForeground;

        [Header("Campagnes")]
        [SerializeField] private CampagnesUI _campagnesObj;

        public void Show(Country p_country)
        {
            _currentCountry = p_country;
            _currentCityDisplayed = null;
            gameObject.SetActive(true);
            _SetCountry();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        //set all informations about the selected country
        private void _SetCountry()
        {
            _countryNameText.text = _currentCountry.name;

            //destroy all buttons in the container before adding new ones
            foreach (Transform t_child in _cityTabsContainer)
            {
                Destroy(t_child.gameObject);
            }
            
            // If country has more than one city, display buttons
            if (_currentCountry.cities.Count > 1)
            {
                _cityTabsContainer.gameObject.SetActive(true);
                foreach (City city in _currentCountry.cities)
                {
                    Button t_button = Instantiate(_cityButtonPrefab, _cityTabsContainer);
                    t_button.GetComponentInChildren<TextMeshProUGUI>().text = city.name;
                    // catch local var to avoid closing problems
                    City t_city = city;
                    t_button.onClick.AddListener(() => _ToggleCityStats(t_city));
                }
            }
            else if (_currentCountry.cities.Count < 1) // if no city, hide buttons container
            {
                _cityTabsContainer.gameObject.SetActive(false);
            }
            else // if only one city, hide buttons container and rename _countryNameText.text by city name

            {
                _cityTabsContainer.gameObject.SetActive(false);
                _countryNameText.text = _currentCountry.cities[0].name;
            }

            _DisplayCountryStats();
        }

        private void _DisplayCountryStats()
        {
            _statVulnerabilityText.text = _currentCountry.GetStat(CityStats.Vulnerability).ToString();
            _statSocialResilienceText.text = _currentCountry.GetStat(CityStats.SocialResilience).ToString();
            _statAdaptabilityText.text = _currentCountry.GetStat(CityStats.Adaptability).ToString();

            //TODO: set the progress bar value dynamically
            _pfasProgressBarForeground.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Horizontal,
                _pfasProgressBarForeground.parent.GetComponent<RectTransform>().rect.width * (_currentCountry.cities.Sum(t_city => t_city.currentContamination) / (100f * _currentCountry.cities.Count))
            );

            _campagnesObj.gameObject.SetActive(false);
        }

        private void _DisplayCityStats(City p_city)
        {
            _statVulnerabilityText.text = p_city.GetStat(CityStats.Vulnerability).ToString();
            _statSocialResilienceText.text = p_city.GetStat(CityStats.SocialResilience).ToString();
            _statAdaptabilityText.text = p_city.GetStat(CityStats.Adaptability).ToString();

            //TODO: set the progress bar value dynamically
            _pfasProgressBarForeground.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Horizontal,
                _pfasProgressBarForeground.parent.GetComponent<RectTransform>().rect.width * (p_city.currentContamination / 100f)
            );

            _campagnesObj.gameObject.SetActive(true);
            _campagnesObj.SetUpCampagne(p_city);
        }

        private void _ToggleCityStats(City p_city)
        {
            //if the city is already displayed, display country stats
            if (_currentCityDisplayed == p_city)
            {
                _currentCityDisplayed = null;
                _DisplayCountryStats();
            }
            else
            {
                _currentCityDisplayed = p_city;
                _DisplayCityStats(p_city);
            }
        }

        //updates stats at each timer tick
        public void UpdateStats()
        {
            if(_currentCityDisplayed == null)
            {
                _DisplayCountryStats();
            }
            else
            {
                _DisplayCityStats(_currentCityDisplayed);
            }
        }
    }
}