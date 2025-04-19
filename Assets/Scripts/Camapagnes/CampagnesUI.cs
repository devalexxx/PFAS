using System;
using System.Linq;
using MyBox;
using PFAS.Stats;
using PFAS.Timer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PFAS.Campagnes
{
    public class CampagnesUI : MonoBehaviour
    {
        [Separator("Choose Campagne")]
        public TMP_Dropdown campagneDropdown;
        public Slider campagneSlider;
        public TextMeshProUGUI moneyText, timeText, amountText;

        [Separator("Campagne Obj")]
        public GameObject chooseCampagne, showCampagne;

        public CampagnesManager manager;

        [Separator("Show Campagne")]
        public Image sliderImage;
        public TextMeshProUGUI campagneTitre;

        int _currentMoneyToUse;

        City _city;
        CityStats _cityStats;

        Campagne _campagne;

        private void Start()
        {
            TimerManager.OnTick += OnDay;
        }

        ~CampagnesUI()
        {
            TimerManager.OnTick -= OnDay;
        }

        public void SetUpCampagne(City p_city)
        {
            _city = p_city;
            var t_campagne = manager.campagnes.FirstOrDefault(c => c.city == _city);
            if (t_campagne != null)
            {
                _campagne = t_campagne;
                ShowCampagne();
            }
            else
            {
                ChooseCampagne();
            }
        }

        private void ShowCampagne()
        {
            chooseCampagne.SetActive(false);
            showCampagne.SetActive(true);

            campagneTitre.text = _campagne.stats.ToString() + " x" + _campagne.amount;

            sliderImage.fillAmount = (float)((float)_campagne.timeBeforeFinish / (float)_campagne.initialTime);
        }

        public void OnDay()
        {
            if (showCampagne.activeSelf && _campagne != null)
            {
                SetUpCampagne(_city);
            }
        }

        private void ChooseCampagne()
        {
            chooseCampagne.SetActive(true);
            showCampagne.SetActive(false);
            _campagne = null;
            _cityStats = CityStats.Adaptability;
            campagneDropdown.ClearOptions();

            foreach (var stat in Enum.GetValues(typeof(CityStats)))
            {
                TMP_Dropdown.OptionData t_option = new TMP_Dropdown.OptionData();
                t_option.text = stat.ToString();
                campagneDropdown.options.Add(t_option);
            }

            campagneSlider.minValue = 0;
            campagneSlider.maxValue = GameManager.instance.money;
            _currentMoneyToUse = 0;
            campagneSlider.value = 0;
            moneyText.text = "0";
            timeText.text = "";
            amountText.text = "0";
        }

        public void SetCurrentMoney(float p_value)
        {
            _currentMoneyToUse = (int)p_value;
            moneyText.text = _currentMoneyToUse.ToString();
            amountText.text = _currentMoneyToUse.ToString();
            timeText.text = _currentMoneyToUse + " j";
        }

        public void SetStat(string p_stat)
        {
            _cityStats = (CityStats)Enum.Parse(typeof(CityStats), campagneDropdown.itemText.text);
        }

        public void AddCampagne()
        {
            Campagne t_campagne = new Campagne(_cityStats, _city, _currentMoneyToUse, _currentMoneyToUse);
            manager.campagnes.Add(t_campagne);
            SetUpCampagne(_city);
        }
    }
}
