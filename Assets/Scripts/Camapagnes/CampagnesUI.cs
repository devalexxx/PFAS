using System;
using PFAS.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PFAS.Campagnes
{
    public class CampagnesUI : MonoBehaviour
    {
        public TMP_Dropdown campagneDropdown;
        public Slider campagneSlider;
        public TextMeshProUGUI moneyText, timeText, amountText;

        public CampagnesManager manager;

        int _currentMoneyToUse;

        City _city;
        CityStats _cityStats;

        public void SetUpCampagne(City p_city)
        {
            _cityStats = CityStats.Adaptability;
            campagneDropdown.ClearOptions(); 

            foreach(var stat in Enum.GetValues(typeof(CityStats)))
            {
                TMP_Dropdown.OptionData t_option = new TMP_Dropdown.OptionData();
                t_option.text = stat.ToString();
                campagneDropdown.options.Add(t_option);
            }

            _city = p_city;

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
        }
    }
}
