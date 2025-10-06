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
        public Slider campagneSlider;
        public TextMeshProUGUI moneyText, timeText;

        [Separator("Campagne Obj")]
        public GameObject chooseCampagne, showCampagne;

        public CampagnesManager manager;

        [Separator("Show Campagne")]
        public Image sliderImage;
        public TextMeshProUGUI campagneEnCoursText;

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

            campagneEnCoursText.text = "Votre campagne pour " + _campagne.stats.ToString() + " se fini dans " + _campagne.amount.ToString() + " jours";

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
            _cityStats = CityStats.Vulnerability;

            campagneSlider.minValue = 0;
            campagneSlider.maxValue = GameManager.instance.money;
            _currentMoneyToUse = 0;
            campagneSlider.value = 0;
            moneyText.text = "0 €";
            timeText.text = "Votre campagne durera ... jours";
        }

        public void SetCurrentMoney(float p_value)
        {
            _currentMoneyToUse = (int)p_value;
            moneyText.text = _currentMoneyToUse.ToString() + " €";
            timeText.text = "Votre campagne durera " + _currentMoneyToUse + " jours";
        }

        public void SetStat(string p_stat)
        {
            _cityStats = (CityStats)Enum.Parse(typeof(CityStats), p_stat);
        }

        public void AddCampagne()
        {
            if (_currentMoneyToUse <= 0) return;

            Campagne t_campagne = new Campagne(_cityStats, _city, _currentMoneyToUse, _currentMoneyToUse);
            manager.campagnes.Add(t_campagne);
            SetUpCampagne(_city);
            GameManager.instance.addMoney(-_currentMoneyToUse);
        }
    }
}
