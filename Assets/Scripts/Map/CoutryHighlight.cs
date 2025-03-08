using UnityEngine;

namespace PFAS.Map
{
    public class CountryHighlight : MonoBehaviour
    {
        // The color of the country when it is selected
        private Color _highlightColor = Color.red;

        // The original color of the country to restore it
        private Color _originalColor;
        private Renderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            if(_spriteRenderer != null)
            {
                _originalColor = _spriteRenderer.material.color;
            }
        }

        public void Highlight()
        {
            if (_spriteRenderer != null)
            {
                _spriteRenderer.material.color = _highlightColor;
            }
        }

        public void RemoveHighlight()
        {
            if (_spriteRenderer != null)
            {
                _spriteRenderer.material.color = _originalColor;
            }
        }
    }
}