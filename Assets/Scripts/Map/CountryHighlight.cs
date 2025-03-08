using UnityEngine;

namespace PFAS.Map
{
    public class CountryOutline : MonoBehaviour
    {
        // The outline of the country
        private GameObject _outline;

        // The scale factor of the country when it is selected
        [SerializeField] private float _selectionScaleFactor = 1.1f;

        private void Awake()
        {
            _outline = transform.Find("Outline").gameObject;

            if(_outline != null)
            {
                _outline.SetActive(false);
            }
        }

        public void Outline()
        {
            if (_outline != null)
            {
                _outline.SetActive(true);

                transform.position += new Vector3(0, 0, -2);
            }
        }

        public void RemoveOutline()
        {
            if (_outline != null)
            {
                _outline.SetActive(false);

                transform.position += new Vector3(0, 0, 2);
            }
        }
    }
}