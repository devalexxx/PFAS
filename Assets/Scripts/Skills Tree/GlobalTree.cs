using System.Collections.Generic;
using UnityEngine;

namespace PFAS.Gameplay
{
    public class GlobalTree : MonoBehaviour
    {

        [HideInInspector]
        public GameObject root, btnPrefab;

        protected List<GameObject> allObject;

        private void Start()
        {
            allObject = new List<GameObject>();
        }

        public virtual void SetUp()
        {
            allObject.ForEach(obj => { Destroy(obj); });
            allObject.Clear();
        }
    }
}