using UnityEngine;

namespace PFAS.Map
{
    public class CountryOutline : MonoBehaviour
    {
        // The outline of the country
        private GameObject _outline;

        // The scale factor of the country when it is selected
        [SerializeField] private float _selectionScaleFactor = 1.1f;
        // The original scale of the country to restore it
        private Vector3 _originalScale;

        private void Awake()
        {
            _outline = transform.Find("Outline").gameObject;

            // Save the original scale of the country
            _originalScale = transform.localScale;

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
                transform.localScale = _originalScale * _selectionScaleFactor;
            }
        }

        public void RemoveOutline()
        {
            if (_outline != null)
            {
                _outline.SetActive(false);

                transform.position += new Vector3(0, 0, 2);
                transform.localScale = _originalScale;
            }
        }
    }
}