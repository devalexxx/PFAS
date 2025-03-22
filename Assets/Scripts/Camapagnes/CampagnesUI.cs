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

        public void SetUpCampagne(City p_city)
        {
            campagneDropdown.ClearOptions(); 

            foreach(var stat in Enum.GetValues(typeof(CityStats)))
            {
                TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
                option.text = stat.ToString();
                campagneDropdown.options.Add(option);
            }
        }
    }
}
