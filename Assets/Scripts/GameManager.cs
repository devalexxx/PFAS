using System.Collections.Generic;
using UnityEngine;

namespace PFAS
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public List<City> cities;

        private void Awake()
        {
            instance = this;
        }
    }
}

